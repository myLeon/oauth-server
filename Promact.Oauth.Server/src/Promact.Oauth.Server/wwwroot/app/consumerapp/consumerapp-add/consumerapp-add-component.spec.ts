﻿declare var describe, it, beforeEach, expect;
import { async, TestBed } from '@angular/core/testing';
import { ConsumerAppModel, consumerappallowedscopes } from "../consumerapp-model";
import { ConsumerappAddComponent } from "../consumerapp-add/consumerapp-add.component";
import { ConsumerAppService } from "../consumerapp.service";
import { Router, RouterModule, Routes } from '@angular/router';
import { Md2Toast } from 'md2';
import { MockToast } from "../../shared/mocks/mock.toast";
import { MockConsumerappService } from "../../shared/mocks/consumerapp/mock.consumerapp.service";
import { MockRouter } from '../../shared/mocks/mock.router';
import { ConsumerAppModule } from '../consumerapp.module';
import { LoaderService } from '../../shared/loader.service';

describe('Consumer Add Test', () => {
    class MockLoaderService { }
    const routes: Routes = [];

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [ConsumerAppModule, RouterModule.forRoot(routes, { useHash: true }) //Set LocationStrategy for component. 
            ],
            providers: [
                { provide: Router, useClass: MockRouter },
                { provide: ConsumerAppService, useClass: MockConsumerappService },
                { provide: Md2Toast, useClass: MockToast },
                { provide: ConsumerAppModel, useClass: ConsumerAppModel },
                { provide: LoaderService, useClass: MockLoaderService },
                { provide: consumerappallowedscopes, useClas: consumerappallowedscopes }
            ]
        }).compileComponents();
    }));

    it("Added consumer app", () => {
        let fixture = TestBed.createComponent(ConsumerappAddComponent); //Create instance of component            
        let consumerappAddComponent = fixture.componentInstance;
        let toast = fixture.debugElement.injector.get(Md2Toast);
        let consumerAppModel = new ConsumerAppModel();
        consumerAppModel.Name = "slack";
        consumerAppModel.LogoutUrl = "www.google.com";
        consumerAppModel.CallbackUrl = "www.google.com";
        consumerAppModel.AuthSecret = "dsdsdsdsdsdsd";
        consumerAppModel.AuthId = "ASASs5454545455";
        consumerAppModel.Scopes = [consumerappallowedscopes.email, consumerappallowedscopes.openid];
        consumerappAddComponent.submitApps(consumerAppModel);
        expect(consumerAppModel.Id).toBe(1);
    });

    it("Random number consumer app AuthId", () => {
        let fixture = TestBed.createComponent(ConsumerappAddComponent); //Create instance of component            
        let consumerappAddComponent = fixture.componentInstance;
        let toast = fixture.debugElement.injector.get(Md2Toast);
        let expectedValue = "SFDASFADSFSAD";
        let consumerAppModel = new ConsumerAppModel();
        consumerappAddComponent.getRandomNumber(true);
        expect(consumerappAddComponent.consumerModel.AuthId).toBe(expectedValue);
    });

    it("Random number consumer app AuthSecret", () => {
        let fixture = TestBed.createComponent(ConsumerappAddComponent); //Create instance of component            
        let consumerappAddComponent = fixture.componentInstance;
        let toast = fixture.debugElement.injector.get(Md2Toast);
        let expectedValue = "SFDASFADSFSAD";
        let consumerAppModel = new ConsumerAppModel();
        consumerappAddComponent.getRandomNumber(false);
        expect(consumerappAddComponent.consumerModel.AuthSecret).toBe(expectedValue);
    });

    it("Consumer app OnInit", () => {
        let fixture = TestBed.createComponent(ConsumerappAddComponent); //Create instance of component            
        let consumerappAddComponent = fixture.componentInstance;
        let toast = fixture.debugElement.injector.get(Md2Toast);
        let expectedValue = "SFDASFADSFSAD";
        let consumerAppModel = new ConsumerAppModel();
        consumerappAddComponent.ngOnInit();
        expect(consumerappAddComponent.consumerModel.AuthSecret).toBe(expectedValue);
    });
});