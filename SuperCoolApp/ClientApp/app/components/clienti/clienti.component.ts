import { Component, Inject, Output, EventEmitter } from '@angular/core';
import { Http, Response, Headers, URLSearchParams, RequestOptions } from '@angular/http';
import { Observable } from "rxjs/Observable";

@Component({
    selector: 'clienti',
    templateUrl: './clienti.component.html',
    styleUrls: ['../caterings/caterings.component.css']

})
export class ClientiComponent {
    public clienti: Cliente[];
    private headers: Headers;

   
    constructor( @Inject(Http) private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        this.http = http;
        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/json');
       
        this.http.get(this.baseUrl + 'api/clienti').subscribe(result => {
            this.clienti = result.json() as Cliente[];
        }, error => console.error(error));

    }



    private inserisciCliente(codice_Fiscale: string, nome: string, cognome: string, citta: string, indirizzo: string, telefono:string) {
        var a = new ClienteClasse(codice_Fiscale, nome, cognome, citta, indirizzo, telefono);
       
        let call = this.http.put(this.baseUrl + 'api/cliente', JSON.stringify(a), { headers: this.headers });

        Observable.forkJoin(call).subscribe(data => {
        a.id = this.clienti.length + 1;
            this.clienti.push(a); }, error => console.error(error));
       
    }


    private aggiornaCliente(id:number,codice: string, nome: string, cognome: string, citta: string, indirizzo: string, telefono: string) {
        var a = new ClienteClasse(codice, nome, cognome, citta, indirizzo, telefono);
        a.id = id;
        let call = this.http.post(this.baseUrl + 'api/cliente', JSON.stringify(a), { headers: this.headers });

        Observable.forkJoin(call).subscribe(data => { }, error => console.error(error));
        for (var _i = 0; _i < this.clienti.length; _i++) {

            if (this.clienti[_i].id == id) {
                this.clienti[_i].citta = citta;
                this.clienti[_i].codice_Fiscale = codice;
                this.clienti[_i].cognome = cognome;
                this.clienti[_i].indirizzo = indirizzo;
                this.clienti[_i].nome = nome;
                this.clienti[_i].telefono = telefono;
                break;
            }


        }


        
    }

    private cancellaCliente(id: number) {

        let call=this.http.delete(this.baseUrl + 'api/cliente?' + "Id=" + id);
        Observable.forkJoin(call).subscribe(data => { }, error => console.error(error));

        for (var _i = 0; _i < this.clienti.length; _i++) {

            if (this.clienti[_i].id == id) {
              
                var b = this.clienti.splice(_i, 1);

                break;

            }


        }
    }




}

interface Cliente {
    id: number;
    codice_Fiscale: string;
    nome: string;
    cognome: string;
    citta: string;
    indirizzo: string;
    telefono: string;
    n_Catering: number;
    spesa_Totale: number;

}
class ClienteClasse implements Cliente {
    id: number;
    codice_Fiscale: string;
    nome: string;
    cognome: string;
    citta: string;
    indirizzo: string;
    telefono: string;
    n_Catering: number;
    spesa_Totale: number;
    constructor(codice: string, nome: string, cognome: string, citta: string, indirizzo: string, telefono: string) {
        this.codice_Fiscale = codice;
        this.nome = nome;
        this.cognome = cognome;
        this.citta = citta;
        this.indirizzo = indirizzo;
        this.telefono = telefono;

    }
}