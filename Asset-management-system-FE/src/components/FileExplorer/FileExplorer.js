import "./FileExplorer.css";
import React from "react";

const FileExplorer = (props) => {
  const { handleImageclick, assets, currentPath } = props;
  console.log(assets);
  return (
    <div className="file-explorer-container">
      <div className="file-uploader-title">
        {" "}
        <b>File Explorer:</b>
      </div>
      <hr />
      <div className="file-explorer-path">
        <b>Current Path: </b>
        <span>{currentPath}</span>
      </div>
      {assets && assets.length > 0 && (
        <div className="file-grid">
          {assets.map((asset, index) => (
            <div className="file-item" onClick={() => handleImageclick(asset)}>
              <img
                src={
                  asset.isFolder
                    ? "https://upload.wikimedia.org/wikipedia/commons/5/59/OneDrive_Folder_Icon.svg"
                    : asset.blobStoragePath
                }
                alt={asset.assetName}
                className="thumbnail"
              />
              <span>{asset.assetName}</span>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};
export default FileExplorer;
