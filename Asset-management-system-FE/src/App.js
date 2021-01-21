import { Route, Switch } from "react-router-dom";
import AssetUploader from "./routes/AssetUploader/AssetUploader";
import AssetDetails from "./routes/AssetDetails/AssetDetails";

import Header from "./components/Header/header";
import "./App.css";

function App() {
  return (
    <div className="App">
      <Header></Header>
      <div className="container">
        <Switch>
          <Route
            path="/"
            exact
            render={() => <AssetUploader></AssetUploader>}
          />
          <Route
            path="/details/:id"
            render={(props) => <AssetDetails {...props}></AssetDetails>}
          />
        </Switch>
      </div>
    </div>
  );
}

export default App;
