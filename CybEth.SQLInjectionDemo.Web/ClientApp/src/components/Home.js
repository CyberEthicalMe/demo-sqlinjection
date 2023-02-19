import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { devices: [], loading: true };
    }

    render() {
        var listing = this.state.loading
            ? <p><em>Loading...</em></p>
            : Home.renderTable(this.state.devices);

        return (
            <div>
                <h1>Science devices for your science fantasies!</h1>
                {listing}
            </div>
        );
    }

    componentDidMount() {
        this.populateData();
    }

    static renderTable(devices) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Price</th>
                    </tr>
                </thead>
                <tbody>
                    {devices.map(d =>
                        <tr key={d.id}>
                            <td>{d.name}</td>
                            <td>{d.price}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    async populateData() {
        const response = await fetch('devices');
        const data = await response.json();
        this.setState({ devices: data, loading: false });
    }
}
