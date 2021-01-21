import React from "react";
import "./header.css";
import logoUrl from "../../logo.svg";
const Header = () => (
  <div className="header">
    <img src={logoUrl} alt="icon" />
    <div className="title">Asset Management System</div>
  </div>
);
export default Header;
