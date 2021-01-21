import React from "react";
import "./FileVariant.css";

const FileVariant = ({ props }) => {
  return (
    <div className="file-variant-container">
      <div className="file-variant-title">
        <b>Asset Details View</b>
      </div>
      <hr />
      <img
        src={props.blobStoragePath}
        alt="Paris"
        className="file-original-varient"
      />
      <div className="file-name">Asset Name: {props.assetName}</div>
      <div className="file-caption">Asset Caption: {props.assetCaption}</div>
      <div className="file-caption">Asset Tags: {props.assetTags}</div>
      <div className="file-variant-title">
        <h4>File Variants: </h4>
      </div>
      <div className="file-variant-list">
        {props.assetVariants &&
          props.assetVariants.length > 0 &&
          props.assetVariants.map((variant) => (
            <div className="file-item">
              <img
                src={variant.blobStoragePath}
                className="thumbnail"
                alt={variant.assetVariantName}
              />
              <span>{variant.assetVariantName}</span>
            </div>
          ))}
      </div>
    </div>
  );
};
export default FileVariant;
