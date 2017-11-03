import { Component, Inject, Output, EventEmitter } from '@angular/core';
import { Http, Response, Headers, URLSearchParams, RequestOptions } from '@angular/http';
import { Observable } from "rxjs/Observable";

@Component({
    selector: 'portate',
    templateUrl: './portate.component.html',
    styleUrls: ['../caterings/caterings.component.css']
})
export class PortateComponent {
    public portate: Portata[];
    public port: PortataClasse;
    private headers: Headers;

    constructor( @Inject(Http) private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        this.http = http;
        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/json');
        
        http.get(baseUrl + 'api/portate').subscribe(result => {
            this.portate = result.json() as Portata[];
        }, error => console.error(error));
    }

    private inserisciPortata(nome: string,costo: number) {
        var a = new PortataClasse( nome, costo);
        let call = this.http.put(this.baseUrl + 'api/portata', JSON.stringify(a), { headers: this.headers });

        Observable.forkJoin(call).subscribe(data => {
            a.id = this.portate.length + 1;
            this.portate.push(a);}, error => console.error(error));
       
    }

    private aggiornaPortata(id: number, nome: string, costo: number) {
        var a = new PortataClasse(nome, costo);
        a.id = id;
        let call = this.http.post(this.baseUrl + 'api/portata', JSON.stringify(a), { headers: this.headers });

        Observable.forkJoin(call).subscribe(data => { }, error => console.error(error));
        for (var _i = 0; _i < this.portate.length; _i++) {

            if (this.portate[_i].id == id) {
                this.portate[_i].costo = costo;
                this.portate[_i].nome = nome;

                break;
            }


        }
    }

    private cancellaPortata(id: number) {

        let call=this.http.delete(this.baseUrl + 'api/portata' + '?id=' + id);
        Observable.forkJoin(call).subscribe(data => { }, error => console.error(error));
        for (var _i = 0; _i < this.portate.length; _i++) {

            if (this.portate[_i].id == id) {
                
                var b = this.portate.splice(_i, 1);

                break;

            }


        }
    }
}

class PortataClasse implements Portata {

    id: number;
    nome: string;
    costo: number;


    constructor( nome: string, costo: number) {
        
        this.nome = nome;
        this.costo = costo;

    }
}

interface Portata {
    id: number;
    nome: String;
    costo: number;

}
