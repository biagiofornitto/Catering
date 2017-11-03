
import { Component, Inject, Output, EventEmitter } from '@angular/core';
import { Http, Response, Headers, URLSearchParams, RequestOptions } from '@angular/http';
import { Observable } from "rxjs/Observable";


@Component({
    selector: 'invitati',
    templateUrl: './invitati.component.html',
    styleUrls: ['../caterings/caterings.component.css']
})
export class InvitatiComponent {
    public invitati: Invitato[];
    private headers: Headers;


    constructor( @Inject(Http) private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        this.http = http;
        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/json');
       
        http.get(baseUrl + 'api/invitati').subscribe(result => {
            this.invitati = result.json() as Invitato[];
        }, error => console.error(error));
    }
    private inserisciInvitato( cognome: string, nome: string, idcat: number) {
        var a = new InvitatiClasse(cognome,nome,idcat);
        let call = this.http.put(this.baseUrl + 'api/invitato', JSON.stringify(a), { headers: this.headers });

        Observable.forkJoin(call).subscribe(data => {
        a.id = this.invitati.length + 1;
            this.invitati.push(a); }, error => console.error(error));
      
    }

    private aggiornaInvitato(id: number, cognome: string, nome: string, idcat: number) {
        var a = new InvitatiClasse(cognome, nome, idcat);
        a.id = id;
        let call = this.http.post(this.baseUrl + 'api/invitato', JSON.stringify(a), { headers: this.headers });

        Observable.forkJoin(call).subscribe(data => { }, error => console.error(error));
        for (var _i = 0; _i < this.invitati.length; _i++) {

            if (this.invitati[_i].id == id) {
                this.invitati[_i].cognome = cognome;
                this.invitati[_i].idcat = idcat;
                this.invitati[_i].nome = nome;
               
                break;
            }


        }
    }

    private cancellaInvitato(id: number) {

       let call= this.http.delete(this.baseUrl + 'api/invitato' + '?id=' + id);
        Observable.forkJoin(call).subscribe(data => { }, error => console.error(error));
        for (var _i = 0; _i < this.invitati.length; _i++) {

            if (this.invitati[_i].id == id) {
               
                var b = this.invitati.splice(_i, 1);

                break;

            }


        }
    }
}

class InvitatiClasse implements Invitato {

    id: number;
    cognome: string;
    nome: string;
    idcat: number;


    constructor( cognome: string, nome: string, idcat: number) {
        
        this.nome = nome;
        this.cognome = cognome;
        this.idcat = idcat;

    }
}

interface Invitato {
    
    id: number;
    cognome: String;
    nome: String;
    idcat: number;
    

}
