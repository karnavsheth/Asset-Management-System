import axios from "axios";
import FileUploader from "../../components/FileUploader/FileUploader";
import FileExplorer from "../../components/FileExplorer/FileExplorer";
import React, { Component } from "react";

class AssetUploader extends Component {
  state = {
    assets: [],
    currentPath: "ParentFolder",
    // Initially, no file is selected
    selectedFile: null,
    isFileReady: false,
    isProcessing: false,
    parentId: 1 //parentFolderId
  };

  componentDidMount() {
    axios
      .get(`http://localhost:50465/api/asset/getassets?parentId=1`)
      .then((res) => {
        const assets = res.data;
        this.setState({ assets });
      });
  }

  handleImageclick = (asset) => {
    console.log("correct");
    if (asset.isFolder === true) {
      axios
        .get(
          `http://localhost:50465/api/asset/getassets?parentId=` + asset.assetId
        )
        .then((res) => {
          const assets = res.data;
          this.setState({
            assets,
            currentPath: this.state.currentPath + "\\" + asset.assetName,
            parentId: asset.assetId
          });
        });
    } else {
      window.location.href = `/details/` + asset.assetId;
    }
  };

  handleFileChange = (event) => {
    // Update the state
    this.setState({ selectedFile: event.target.files[0], isFileReady: true });
  };

  // On file upload (click the upload button)
  handleFileUpload = () => {
    if (this.state.selectedFile) {
      const formData = new FormData();
      // Update the formData object
      formData.append(
        "file",
        this.state.selectedFile,
        this.state.selectedFile.name
      );

      formData.append("parentId",this.state.parentId)

      // Request made to the backend api
      this.setState({ isProcessing: true });
      axios
        .post("http://localhost:50465/api/uploadfile", formData)
        .then((msg) => {
          this.setState({ isProcessing: false });
          window.location.reload();
        })
        .catch((ex) => {
          this.setState({ isProcessing: false });
        });
    }

    return;
  };

  // File content to be displayed
  fileData = () => {
    if (this.state.selectedFile) {
      return (
        <div>
          <h4>File Details:</h4>
          <p>File Name: {this.state.selectedFile.name}</p>
          <p>File Type: {this.state.selectedFile.type}</p>
          <p>
            Last Modified:{" "}
            {this.state.selectedFile.lastModifiedDate.toDateString()}
          </p>
        </div>
      );
    } else {
      return (
        <div>
          <br />
          <h4>Choose File before Pressing the Upload button</h4>
        </div>
      );
    }
  };

  render() {
    return (
      <div>
        <FileUploader
          handleFileChange={this.handleFileChange}
          handleFileUpload={this.handleFileUpload}
          handleFileMetadata={this.fileData}
          isProcessing={this.state.isProcessing}
          isFileReady={this.state.isFileReady}
        />
        <FileExplorer
          assets={this.state.assets}
          currentPath={this.state.currentPath}
          handleImageclick={this.handleImageclick}
        />
      </div>
    );
  }
}

export default AssetUploader;
