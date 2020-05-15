class UserBox extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            data: this.props.initialData
        };
        this.handleUserSubmit = this.handleUserSubmit.bind(this);
    }

    loadUsersFromServer() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        };
        xhr.send();
    }

    handleUserSubmit(user) {
        const users = this.state.data;
        
        user.id = users.length + 1;
        const newUsers = users.concat([user]);
        this.setState({ data: newUsers });

        const data = new FormData();
        data.append('Name', user.name);
        data.append('LastName', user.lastName);
        const xhr = new XMLHttpRequest();
        xhr.open('post', this.props.submitUrl, true);
        xhr.onload = () => this.loadUsersFromServer();
        xhr.send(data);

    }

    componentDidMount() {
        window.setInterval(
            () => this.loadUsersFromServer(),
            this.props.pollInterval,
        );
    }

    render() {
        return (
            <div className="UserBox">
                <h1>UserBox</h1>
                <UserForm onUserSubmit={this.handleUserSubmit} />
                <UserList data={this.state.data} />
            </div>
        );
    }


}

function createRemarkable() {
    var remarkable =
        'undefined' !== typeof global && global.Remarkable
            ? global.Remarkable
            : window.Remarkable;

    return new remarkable();
}

class UserList extends React.Component {
    render() {
        const userNodes = this.props.data.map(user => (
            <User name={user.name} Id={user.id}>{user.lastName}</User>
        ));
        return (
            <div className="userList">
                {userNodes}
            </div>
        );
    }
}

class User extends React.Component {
    rawMarkup() {
        const md = new createRemarkable();
        const rawMarkup = md.render(this.props.children.toString());
        return { __html: rawMarkup };
    }

    render() {
        return (
            <div className="user">
                <h2 className="userName">{this.props.name}</h2>
                <span dangerouslySetInnerHTML={this.rawMarkup()} />
            </div>
        );
    }
}

class UserForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            name: '',
            lastName: ''
        };
        this.handleNameChange = this.handleNameChange.bind(this);
        this.handleLastNameChange = this.handleLastNameChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleNameChange(e) {
        this.setState({ name: e.target.value });
    }

    handleLastNameChange(e) {
        this.setState({ LastName: e.target.value });
    }

    handleSubmit(e) {
        e.preventDefault();
        const name = this.state.name.trim();
        const LastName = this.state.LastName.trim();
        if (!name || !LastName) {
            return;
        }
        this.props.onUserSubmit({ name: name, lastName: LastName });
        this.setState({ name: '', LastName: '' });
    }


    render() {
        return (
            <div className="userForm" onSubmit={this.handleSubmit}>
                <form className="userForm">
                    <input type="text" onChange={this.handleNameChange} value={this.state.name} placeholder="Your name" />
                    <input type="text" onChange={this.handleLastNameChange} value={this.state.LastName} placeholder="You LastName" />
                    <input type="submit" value="Post" />
                </form>
            </div>
        );
    }
}



//ReactDOM.render(
//    <CommentBox
//        url="/comments"
//        pollInterval={20000}
//        submitUrl="/comments/new"
//    />,
//    document.getElementById('content')
//);

