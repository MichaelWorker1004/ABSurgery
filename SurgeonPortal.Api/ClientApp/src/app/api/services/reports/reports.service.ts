import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ReportService {

    constructor( private httpClient: HttpClient) {}

 
        public downloadAdmissionCard(examCode: string, type: string, accessToken: string,
        apiVersion = '1.0'):void {
            
            
            const header = new HttpHeaders().set('Authorization',  `Bearer ${accessToken}`);
            const report = this.httpClient.get<any>(`api/examinations/admission-card/document?examCode=${examCode}&type=${type}&api-version=${apiVersion}`, {headers: header, observe: 'response', responseType: 'blob' as 'json'});
            
            report.subscribe((response) => {
                const url = window.URL.createObjectURL(response.body);
                window.open(url);
                URL.revokeObjectURL(url);
            });
        }

        public downloadInvoice(invoiceNumber: string,
        apiVersion = '1.0'):void {
            
            
            //const header = new HttpHeaders().set('Authorization',  `Bearer ${accessToken}`);
            const report = this.httpClient.get<any>(`api/reports/invoice?invoiceNumber=${invoiceNumber}&api-version=${apiVersion}`, {observe: 'response', responseType: 'blob' as 'json'});
            
            report.subscribe((response) => {
                const url = window.URL.createObjectURL(response.body);
                window.open(url);
                URL.revokeObjectURL(url);
            });
        }
}
