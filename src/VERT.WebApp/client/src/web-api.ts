import { HttpClient, json } from "aurelia-fetch-client";
import { autoinject } from "aurelia-framework";

let latency = 200;
let id = 0;

function getId() {
    return ++id;
}

let contacts = [
    {
        id: getId(),
        firstName: 'John',
        lastName: 'Tolkien',
        email: 'tolkien@inklings.com',
        phoneNumber: '867-5309'
    },
    {
        id: getId(),
        firstName: 'Clive',
        lastName: 'Lewis',
        email: 'lewis@inklings.com',
        phoneNumber: '867-5309'
    },
    {
        id: getId(),
        firstName: 'Owen',
        lastName: 'Barfield',
        email: 'barfield@inklings.com',
        phoneNumber: '867-5309'
    },
    {
        id: getId(),
        firstName: 'Charles',
        lastName: 'Williams',
        email: 'williams@inklings.com',
        phoneNumber: '867-5309'
    },
    {
        id: getId(),
        firstName: 'Roger',
        lastName: 'Green',
        email: 'green@inklings.com',
        phoneNumber: '867-5309'
    }
];

@autoinject
export class WebAPI {
    isRequesting = false;

    constructor(private client: HttpClient) {
        this.configureHttpClient(client);
    }

    private configureHttpClient(client: HttpClient) {
        client.configure(config => {
            config
                .withBaseUrl('http://localhost:24821/api/')
                .withDefaults({
                    credentials: 'same-origin',
                    headers: {
                        'Accept': 'application/json',
                        'X-Requested-With': 'Fetch'
                    }
                })
                .withInterceptor({
                    request(request) {
                        console.log(`Requesting ${request.method} ${request.url}`);
                        //let authHeader = fakeAuthService.getAuthHeaderValue(request.url);
                        //request.headers.append('Authorization', authHeader);
                        return request;
                    },
                    response(response) {
                        console.log(`Received ${response.status} ${response.url}`);
                        return response;
                    }
                });
        });
    }

    getContacts() {
        this.isRequesting = true;
        return new Promise(resolve => {
            let results = this.client
                .fetch("contacts", {
                    method: "get"
                })
                .then(response => {
                    this.isRequesting = false;
                    return response.json()
                });
            resolve(results);
        });
    }

    getContactList() {
        this.isRequesting = true;
        return new Promise(resolve => {
            setTimeout(() => {
                let results = contacts.map(x => {
                    return {
                        id: x.id,
                        firstName: x.firstName,
                        lastName: x.lastName,
                        email: x.email
                    }
                });
                resolve(results);
                this.isRequesting = false;
            }, latency);
        });
    }

    getContactDetails(id) {
        this.isRequesting = true;
        return new Promise(resolve => {
            setTimeout(() => {
                let found = contacts.filter(x => x.id == id)[0];
                resolve(JSON.parse(JSON.stringify(found)));
                this.isRequesting = false;
            }, latency);
        });
    }

    saveContact(contact) {
        this.isRequesting = true;
        return new Promise(resolve => {
            setTimeout(() => {
                let instance = JSON.parse(JSON.stringify(contact));
                let found = contacts.filter(x => x.id == contact.id)[0];

                if (found) {
                    let index = contacts.indexOf(found);
                    contacts[index] = instance;
                } else {
                    instance.id = getId();
                    contacts.push(instance);
                }

                this.isRequesting = false;
                resolve(instance);
            }, latency);
        });
    }
}
