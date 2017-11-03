/*routing interno dell'app*/
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { CateringsComponent } from './components/caterings/caterings.component';
import { ClientiComponent } from './components/clienti/clienti.component';
import { MenusComponent } from './components/menus/menus.component';
import { PortateComponent } from './components/portate/portate.component';
import { RistorantiComponent } from './components/ristoranti/ristoranti.component';
import { InvitatiComponent } from './components/invitati/invitati.component';
@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        CateringsComponent,
        ClientiComponent,
        MenusComponent,
        PortateComponent,
        RistorantiComponent,
        InvitatiComponent

    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'caterings', component: CateringsComponent },
            { path: 'clienti', component: ClientiComponent },
            { path: 'menus', component: MenusComponent },
            { path: 'ristoranti', component: RistorantiComponent },
            { path: 'portate', component: PortateComponent },
            { path: 'invitati', component: InvitatiComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
