
# A sample react component

Here we have a checkbox that, when clicked, calls its parent component and updates its state.


```js
import React from "react";
import ReactDOM from "react-dom";

import "./styles.css";

class RememberSelected extends React.Component {
  constructor(props) {
    super(props);
    this.handleChangeState = this.handleChangeState.bind(this);
    this.state = {
      checked: false,
      label: this.props.label
    };
  }

  handleChangeState() {
    const st = this.state
    this.setState({
      checked: !st.checked
    });
    this.props.onChange(!st.checked)
  }

  render() {
    return (
      <label>
        <input
          type="checkbox"
          checked={this.state["checked"]}
          onChange={this.handleChangeState}
        />
        {this.state["label"]}
      </label>
    );
  }
}

class Container extends React.Component {

  constructor(props) {
    super(props)
    this.handleClick = this.handleClick.bind(this);
    this.state = {
      checked: false
    }
  }

  handleClick(state) {
    this.setState({ checked: state });
    console.log("read " + state);
  }

  render() {
    return (
      <div>
        <RememberSelected 
          label="Hello World" 
          onChange={this.handleClick}
          checked={this.checked} />
      </div>
    );
  }
}

function App() {
  return (
    <div className="App">
      <h1>Testing</h1>
      <Container />
    </div>
  );
}

const rootElement = document.getElementById("root");
ReactDOM.render(<App />, rootElement);
```

Use https://codesandbox.io/ for testing.
