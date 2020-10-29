export class CreateAccountChart {
    constructor(public from: Date, public to: Date){};
}

export interface ChartResponse {
    chartData: {[key: string]: string};
}

export interface KeyValuePair {
    key: any;
    value: any;
}