import { Component, Inject, Output, EventEmitter } from '@angular/core';
import { Http, Response, Headers, URLSearchParams, RequestOptions } from '@angular/http';
import { Observable } from "rxjs/Observable";

@Component({
    selector: 'ristoranti',
    templateUrl: './ristoranti.component.html',
    styleUrls: ['../caterings/caterings.component.css']
})
export class RistorantiComponent {
    public ristoranti: Ristorante[];

    
    public rist: RistoranteClasse;
    private headers: Headers;


    constructor( @Inject(Http) private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        this.http = http;
        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/json');
        
        this.http.get(this.baseUrl + 'api/ristoranti').subscribe(result => {
            this.ristoranti = result.json() as Ristorante[];
        }, error => console.error(error));

    }



    private inserisciRistorante(piva: string, indirizzo: string) {
        var a = new RistoranteClasse('user'+piva,piva,indirizzo);
        let call = this.http.put(this.baseUrl + 'api/ristorante', JSON.stringify(a), { headers: this.headers });

        Observable.forkJoin(call).subscribe(data => {
             a.id = this.ristoranti.length + 1;
            this.ristoranti.push(a); }, error => console.error(error));
        
    }

    private aggiornaRistorante(id:number,piva: string, indirizzo: string) {
        var a = new RistoranteClasse('user' + piva, piva, indirizzo);
        a.id = id;
        let call = this.http.post(this.baseUrl + 'api/ristorante', JSON.stringify(a), { headers: this.headers });

        Observable.forkJoin(call).subscribe(data => { }, error => console.error(error));
        for (var _i = 0; _i < this.ristoranti.length; _i++) {

            if (this.ristoranti[_i].id == id) {
                this.ristoranti[_i].indirizzo = indirizzo;
                this.ristoranti[_i].piva = piva;
                

                break;
            }


        }
    }

    private cancellaRistorante(id: number) {
        
        let call=this.http.delete(this.baseUrl + 'api/ristorante' + '?Id=' + id);
        Observable.forkJoin(call).subscribe(data => { }, error => console.error(error));
        for (var _i = 0; _i < this.ristoranti.length; _i++) {

            if (this.ristoranti[_i].id == id) {
                
                var b = this.ristoranti.splice(_i, 1);

                break;

            }


        }
    }


}




class RistoranteClasse implements Ristorante {

    id: number;
    username: string;
    piva: string;
    indirizzo: string;
    

    constructor(username: string,piva: string,indirizzo:string) {
        this.username = username;
        this.piva = piva;
        this.indirizzo = indirizzo;

    }
}



interface Ristorante {
    id: number;
    username: String;
    piva: String;
    indirizzo: String;
    

}



