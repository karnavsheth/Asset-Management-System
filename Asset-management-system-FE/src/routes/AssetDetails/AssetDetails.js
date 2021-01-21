import axios from "axios";
import React, { Component } from "react";
import "./AssetDetails.css";
import FileVariant from "../../components/FileVariant/FileVariant.js";

class AssetDetails extends Component {
  state = {
    AssetDetails: {},
    isLoaded: false,
  };

  componentDidMount() {
    axios
      .get(
        ` http://localhost:50465/api/asset/getassetdetails?assetId=` +
          this.props.match.params.id
      )
      .then((res) => {
        const assetDetails = res.data;
        this.setState({ AssetDetails: assetDetails, isLoaded: true });
      });
  }

  render() {
    return (
      <>
        {this.state.isLoaded && (
          <div>
            {this.state.AssetDetails !== "" ? (
              <FileVariant props={this.state.AssetDetails} />
            ) : (
              <div className="no-data">No Data for this Asset ID</div>
            )}
          </div>
        )}
      </>
    );
  }
}
export default AssetDetails;
