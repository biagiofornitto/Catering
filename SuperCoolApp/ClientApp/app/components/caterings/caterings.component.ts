import { Component, Inject, Output, EventEmitter } from '@angular/core';
import { Http, Response, Headers, URLSearchParams, RequestOptions } from '@angular/http';
import 'rxjs/add/observable/forkJoin';
import { Observable } from "rxjs/Observable";
@Component({
    selector: 'caterings',
    templateUrl: './caterings.component.html',
    styleUrls: ['./caterings.component.css']
})
export class CateringsComponent {
    public caterings: Catering[];
    public cat: CateringClasse;
    private headers: Headers;

   
    constructor( private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        this.http = http;
        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/json');
    
        this.http.get(this.baseUrl + 'api/caterings').subscribe(result => {
            this.caterings = result.json() as Catering[];
        }, error => console.error(error));
        
    }



    async inserisciCatering(codice: number, data: Date, cliente: string, menu: number, partecipanti: number, costo: number): Promise<void> {
        var a = new CateringClasse(codice,cliente,costo,data, menu, partecipanti);
        
        let call = this.http.put(this.baseUrl + 'api/catering', JSON.stringify(a), { headers: this.headers });
        
        Observable.forkJoin(call).subscribe(data => {
             a.id = this.caterings.length + 1;
            this.caterings.push(a);}, error => console.error(error));
       
    }


    private aggiornaCatering(id:number,codice: number, data: Date, cliente: string, menu: number, partecipanti: number, costo: number) {
        
        var a = new CateringClasse(codice, cliente, costo, data, menu, partecipanti);
        a.id = id;
        let call = this.http.post(this.baseUrl + 'api/catering', JSON.stringify(a), { headers: this.headers });

        Observable.forkJoin(call).subscribe(data => { }, error => console.error(error));
        
        for (var _i = 0; _i < this.caterings.length; _i++) { 
           
            if (this.caterings[_i].id == id) {
                this.caterings[_i].cliente = cliente;
                this.caterings[_i].codice = codice;
                this.caterings[_i].costoT = costo;
                this.caterings[_i].data = data;
                this.caterings[_i].menu = menu;
                this.caterings[_i].numeroP = partecipanti;
                break;
            }

           
        }
        
    }

    private cancellaCatering(id: number) {

        let call = this.http.delete(this.baseUrl + 'api/catering' + '?Id=' + id);

        Observable.forkJoin(call).subscribe(data => { }, error => console.error(error));

        for (var _i = 0; _i < this.caterings.length; _i++) {

            if (this.caterings[_i].id == id) {
              
                var b = this.caterings.splice(_i,1);
                
                break;

            }


        }
    }

   



}


interface Catering {
    id: number;
    codice: number;
    data: Date;
    cliente: String;
    menu: number;
    numeroP: number;
    costoT: number;

}

class CateringClasse implements Catering {
    id: number;
    codice: number;
    data: Date;
    cliente: String;
    menu: number;
    numeroP: number;
    costoT: number;

    constructor(codice: number,cliente: String, costoT: number, data: Date,  menu: number, numeroP: number) {
        this.codice = codice;
        this.data = data;
        this.cliente = cliente;
        this.menu = menu;
        this.numeroP = numeroP;
        this.costoT = costoT;

    }
 }