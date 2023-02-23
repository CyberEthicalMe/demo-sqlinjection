import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;
    static defaultQuery = 'order by cost desc';

    constructor(props) {
        super(props);
        this.state = { query: Home.defaultQuery, devices: [], loading: true, error: null };

        this.handleChange = this.handleChange.bind(this);
        this.runQuery = this.runQuery.bind(this);
    }

    render() {
        var listing = this.state.loading
            ? <p><em>Loading...</em></p>
            : Home.renderTable(this.state.devices);

        return (
            <div>
                <h1>Science devices for your science fantasies!</h1>
                <label>
                    Filter expression:<br />
                    <textarea name="query" value={this.state.query} onChange={this.handleChange}></textarea><br />
                    <div style={{ color: 'darkred' }}>{this.state.error}</div>
                </label><br />
                <input type="button" value="Filter" onClick={this.runQuery} />
                {listing}
            </div>
        );
    }

    componentDidMount() {
        this.populateData();
    }

    runQuery(event) {
        this.populateData();
    }

    handleChange(event) {
        this.setState({ query: event.target.value });
    }

    static renderTable(devices) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Cost</th>
                    </tr>
                </thead>
                <tbody>
                    {devices.map(d =>
                        <tr key={d.id}>
                            <td>{d.name}</td>
                            <td>{d.cost}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    async populateData() {
        this.setState({ devices: [], loading: true, error: null });

        fetch('devices?filterQuery=' + this.state.query)
            .then(async response => {
                const data = await response.json();

                if (!response.ok) {
                    const error = (data && data.Reason) || response.statusText;
                    this.setState({ loading: false, error: error });
                }
                else {
                    this.setState({ devices: data, loading: false });
                }
            })
    }
}
