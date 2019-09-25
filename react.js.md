
# A sample react component

Here we have a checkbox that, when clicked, calls its parent component and updates its state.

It is recommended for child components to be stateless. They can receive updates via their props, which 
are updated by their parent's state. In the following example, MyCheckbox is stateless. It's label and
value get updated when its parent changes its state in handleClick(value), which happens to be called
by MyCheckbox.

```js
import React from "react";
import ReactDOM from "react-dom";

import "./styles.css";

class MyCheckbox extends React.Component {
  constructor(props) {
    super(props);
    this.handleChangeState = this.handleChangeState.bind(this);
  }

  handleChangeState() {
    this.props.onChange(!this.props.checked)
  }

  render() {
    return (
      <label>
        <input
          type="checkbox"
          checked={this.props.checked}
          onChange={this.handleChangeState}
        />
        {this.props.label + " " + this.props.checked}
      </label>
    );
  }
}

class Container extends React.Component {

  constructor(props) {
    super(props)
    this.handleClick = this.handleClick.bind(this);
    this.state = {
      checked: true
    }
  }

  handleClick(value) {
    this.setState((state, props) => { 
      return { checked: value }
      // could be the following as well:
      // return { checked: !state.checked }
    });
  }

  render() {
    return (
      <div>
        <MyCheckbox 
          label="Is box checked?" 
          onChange={this.handleClick}
          checked={this.state.checked} />
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

Here is another example, this time using a textbox to update the contents of a div. Both child components are stateless.

```js
import React from "react";
import ReactDOM from "react-dom";

import "./styles.css";

class MyTextbox extends React.Component {

  constructor(props) {
    super(props)
    this.handleChange = this.handleChange.bind(this)
  }

  handleChange(evt) {
    this.props.onChange(evt.target.value)
  }

  render() {
    return (
      <div>
        <label>{this.props.label}</label>
        <input 
          type="textbox"
          onChange={evt => this.handleChange(evt)}
          />
      </div>
    )
  }

}

class MyLabel extends React.Component {
  render() {
    return (
      <div>{this.props.label}</div>
    )
  }
}

class Container extends React.Component {

  constructor(props) {
    super(props)
    this.handleChange = this.handleChange.bind(this);
    this.state = {
      label: ""
    }
  }

  handleChange(value) {
    this.setState((state, props) => { 
      return { label: value }
    });
  }

  render() {
    return (
      <div>
        <MyTextbox 
          label="Type something:" 
          onChange={this.handleChange} />
        <MyLabel
          label={this.state.label} />
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
