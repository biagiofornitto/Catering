import { Component, Inject, Output, EventEmitter } from '@angular/core';
import { Http, Response, Headers, URLSearchParams, RequestOptions } from '@angular/http';
import { Observable } from "rxjs/Observable";

@Component({
    selector: 'menus',
    templateUrl: './menus.component.html',
    styleUrls: ['../caterings/caterings.component.css']
})
export class MenusComponent {
    public menus: Menu[];
    public menu: MenuClasse;
    private headers: Headers;

    constructor( @Inject(Http) private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        this.http = http;
        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/json');
        
        http.get(baseUrl + 'api/menu').subscribe(result => {
            this.menus = result.json() as Menu[];
        }, error => console.error(error));
    }

    private inserisciMenu( costoTot: number, ristorante: number) {
      
        var a = new MenuClasse( ristorante, costoTot);
        let call = this.http.put(this.baseUrl + 'api/menu', JSON.stringify(a), { headers: this.headers });

        Observable.forkJoin(call).subscribe(data => {
             a.id = this.menus.length + 1;
            this.menus.push(a); }, error => console.error(error));
       
    }

    private aggiornaMenu(id: number, costoTot: number, ristorante: number) {
        var a = new MenuClasse(ristorante, costoTot);
        a.id = id;
        let call = this.http.post(this.baseUrl + 'api/menu', JSON.stringify(a), { headers: this.headers });

        Observable.forkJoin(call).subscribe(data => { }, error => console.error(error));
        for (var _i = 0; _i < this.menus.length; _i++) {

            if (this.menus[_i].id == id) {
                this.menus[_i].costo_Totale = costoTot;
                this.menus[_i].idRistorante = ristorante;

                break;
            }


        }
    }

    private cancellaMenu(id: number) {

       let call= this.http.delete(this.baseUrl + 'api/menu' + '?id=' + id);
        Observable.forkJoin(call).subscribe(data => { }, error => console.error(error));
        for (var _i = 0; _i < this.menus.length; _i++) {

            if (this.menus[_i].id == id) {
                
                var b = this.menus.splice(_i, 1);

                break;

            }


        }
    }
}

class MenuClasse implements Menu {

    id: number;
    idRistorante: number;
    costo_Totale: number;


    constructor( idRistorante:number, costoTot:number) {
        
        this.idRistorante = idRistorante;
        this.costo_Totale = costoTot;

    }
}

interface Menu {
    id: number;
    idRistorante: number;
    costo_Totale: number;

}
