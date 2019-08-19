import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface FetchPasswordState {
    responses: PasswordResponse[];
    loading: boolean;
}

export class FetchPassword extends React.Component<RouteComponentProps<{}>, FetchPasswordState> {
    constructor() {
        super();
        this.state = { responses: [], loading: true };

        fetch('/api/config')
            .then(response => response.json() as Promise<ConfigResponse>)
            .then(data => {
                let i = 0;
                while (i < 3) {
                    fetch(data.apiUrl + '/password')
                        .then(response => response.json() as Promise<PasswordResponse>)
                        .then(data => {
                            this.setState({ responses: this.state.responses.concat(data), loading: false });
                        });
                    i++;
                }
            });
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchPassword.renderTable(this.state.responses);

        return <div>
            <h1>Password Generator</h1>
            {contents}
        </div>;
    }

    private static renderTable(responses: PasswordResponse[]) {
        return <table className='table'>
            <thead>
                <tr>
                    <th>Suggested Password</th>
                    <th>API Version</th>
                    <th>API Server</th>
                </tr>
            </thead>
            <tbody>
                {responses.map(response =>
                    <tr key={response.password}>
                        <td>{response.password}</td>
                        <td>{response.apiVersion}</td>
                        <td>{response.apiServer}</td>
                    </tr>
                )}
            </tbody>
        </table>;
    }
}

interface PasswordResponse {
    password: string;
    apiVersion: string;
    apiServer: string;
}

interface ConfigResponse {
    apiUrl: string;
}
