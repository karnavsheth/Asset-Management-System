import React from "react";
import "./FileUploader.css";

var FileUploader = (props) => {
  const {
    handleFileChange,
    handleFileUpload,
    handleFileMetadata,
    isFileReady, 
    isProcessing,
  } = props;

  return (
    <div className="file-uploader-container">
      <div className="file-uploader-title">
        <b>File Upload:</b>
      </div>
      <hr />
      <div className="file-upload-section">
        <div>
          <input type="file" onChange={handleFileChange} />
          <button onClick={handleFileUpload} disabled={!isFileReady}>
            Upload File
          </button>
        </div>
        {handleFileMetadata()}
        {isProcessing && <div class="loader"></div>}
      </div>
    </div>
  );
};

export default FileUploader;
