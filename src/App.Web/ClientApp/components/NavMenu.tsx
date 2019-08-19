import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link, NavLink } from 'react-router-dom';

interface NavMenuState {
    version: string;
    server: string;
}

export class NavMenu extends React.Component<{}, NavMenuState> {
    constructor() {
        super();
        this.state = { version: "x.y", server: "" };

            fetch('/api/diagnostics')
                .then(response => response.json() as Promise<DiagnosticsResponse>)
                .then(data => {
                    this.setState({ version: data.version, server: data.server});
                });
    }

    public render() {
        return <div className='main-nav'>
                <div className='navbar navbar-inverse'>
                <div className='navbar-header'>
                    <button type='button' className='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'>
                        <span className='sr-only'>Toggle navigation</span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                    </button>
                    <Link className='navbar-brand' to={ '/' }>Website</Link>
                    <p className='navbar-brand'>Version: {this.state.version}</p>
                    <p className='navbar-brand'>Server: {this.state.server}</p>
                </div>
                <div className='clearfix'></div>
                <div className='navbar-collapse collapse'>
                    <ul className='nav navbar-nav'>
                        <li>
                            <NavLink to={ '/' } exact activeClassName='active'>
                                <span className='glyphicon glyphicon-home'></span> Home
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={ '/password' } activeClassName='active'>
                                <span className='glyphicon glyphicon-th-list'></span> Password Generator
                            </NavLink>
                        </li>
                    </ul>
                </div>
            </div>
        </div>;
    }
}

interface DiagnosticsResponse {
    version: string;
    server: string;
}
