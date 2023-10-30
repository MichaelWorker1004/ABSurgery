import { Injectable } from '@angular/core';
import { catchError, share, tap } from 'rxjs/operators';
import { forkJoin, map, Observable, of } from 'rxjs';
import { Action, State, StateContext, StateToken, Store } from '@ngxs/store';

import {
  ICountryReadOnlyModel,
  IEthnicityReadOnlyModel,
  IGenderReadOnlyModel,
  ILanguageReadOnlyModel,
  IRaceReadOnlyModel,
  IStateReadOnlyModel,
  IClinicalLevelReadOnlyModel,
  IClinicalActivityReadOnlyModel,
  PicklistsService,
  ILicenseTypeReadOnlyModel,
  IPracticeTypeReadOnlyModel,
  IOrganizationTypeReadOnlyModel,
  IAppointmentTypeReadOnlyModel,
  IJcahoOrganizationReadOnlyModel,
  IPrimaryPracticeReadOnlyModel,
  IScoringSessionReadOnlyModel,
} from '../../api';
import {
  GetAccreditedProgramInstitutionsList,
  GetCountryList,
  GetDegrees,
  GetEthnicityList,
  GetFellowshipPrograms,
  GetGenderList,
  GetGraduateProfiles,
  GetLanguageList,
  GetPicklists,
  GetRaceList,
  GetResidencyPrograms,
  GetStateList,
  GetTrainingTypeList,
  GetClinicalLevelList,
  GetClinicalActivityList,
  GetCertificateTypes,
  GetDocumentTypes,
  GetLicenseTypeList,
  GetPracticeTypeList,
  GetOrganizationTypeList,
  GetAppointmentTypeList,
  GetJcahoOrganizationList,
  GetPrimaryPracticeList,
  GetScoringSessionList,
  GetFellowshipTypes,
} from './picklists.actions';
import { IFormErrors } from '../../shared/common';
import { IAccreditedProgramInstitutionReadOnlyModel } from 'src/app/api/models/picklists/accredited-program-institution-read-only.model';
import { ITrainingTypeReadOnlyModel } from 'src/app/api/models/picklists/training-type-read-only.model';
import { IGraduateProfileReadOnlyModel } from 'src/app/api/models/picklists/graduate-profile-read-only.model';
import { IDegreeReadOnlyModel } from 'src/app/api/models/picklists/degree-read-only.model';
import { IFellowshipProgramReadOnlyModel } from 'src/app/api/models/picklists/fellowship-program-read-only.model';
import { IResidencyProgramReadOnlyModel } from 'src/app/api/models/picklists/residency-program-read-only.model';
import { ICertificateTypeReadOnlyModel } from 'src/app/api/models/picklists/certificate-type-read-only.model';
import { IDocumentTypeReadOnlyModel } from 'src/app/api/models/picklists/document-type-read-only.model';
import { IFellowshipTypeReadOnlyModel } from 'src/app/api/models/picklists/fellowship-type-read-only.model';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';
export interface IPicklist {
  countries: ICountryReadOnlyModel[] | undefined;
  ethnicities: IEthnicityReadOnlyModel[] | undefined;
  genders: IPickListItem[] | undefined;
  languages: IPickListItem[] | undefined;
  races: IRaceReadOnlyModel[] | undefined;
  states: IStateReadOnlyModel[] | undefined;
  statesMap: { [key: string]: IStateReadOnlyModel[] };
  defaultStates: IStateReadOnlyModel[] | undefined;
  accreditedInstitutions:
    | IAccreditedProgramInstitutionReadOnlyModel[]
    | undefined;
  trainingTypes: ITrainingTypeReadOnlyModel[] | undefined;
  graduateProfiles: IGraduateProfileReadOnlyModel[] | undefined;
  degrees: IDegreeReadOnlyModel[] | undefined;
  fellowshipPrograms: IFellowshipProgramReadOnlyModel[] | undefined;
  residencyPrograms: IResidencyProgramReadOnlyModel[] | undefined;
  clinicalLevels: IClinicalLevelReadOnlyModel[] | undefined;
  clinicalActivities: IClinicalActivityReadOnlyModel[] | undefined;
  certificateTypes: ICertificateTypeReadOnlyModel[] | undefined;
  documentTypes: IDocumentTypeReadOnlyModel[] | undefined;
  licenseTypes: IPickListItemNumber[] | undefined;
  practiceTypes: IPickListItemNumber[] | undefined;
  organizationTypes: IPickListItemNumber[] | undefined;
  appointmentTypes: IPickListItemNumber[] | undefined;
  jcahoOrganizations: IPickListItemNumber[] | undefined;
  primaryPractices: IPickListItemNumber[] | undefined;
  scoringSessions: IScoringSessionReadOnlyModel[] | undefined;
  fellowshipTypes: IFellowshipTypeReadOnlyModel[] | undefined;
  errors?: IFormErrors | undefined;
}

export interface IPickListItem {
  itemValue: string | null | undefined;
  itemDescription: string | null | undefined;
  modifier?: string | null | undefined;
  isCredit?: boolean | null | undefined;
  isEssential?: boolean | null | undefined;
}
export interface IPickListItemNumber {
  itemValue: number | null | undefined;
  itemDescription: string | null | undefined;
  modifier?: string | null | undefined;
  isCredit?: boolean | null | undefined;
  isEssential?: boolean | null | undefined;
}

export interface IPicklistUserValues {
  countries: ICountryReadOnlyModel[] | undefined;
  ethnicities: IEthnicityReadOnlyModel[] | undefined;
  genders: IPickListItem[] | undefined;
  languages: IPickListItem[] | undefined;
  races: IRaceReadOnlyModel[] | undefined;
  states: IStateReadOnlyModel[] | undefined;
  statesMap: { [key: string]: IStateReadOnlyModel[] } | undefined;
  defaultStates: IStateReadOnlyModel[] | undefined;
  accreditedInstitutions:
    | IAccreditedProgramInstitutionReadOnlyModel[]
    | undefined;
  trainingTypes: ITrainingTypeReadOnlyModel[] | undefined;
  graduateProfiles: IGraduateProfileReadOnlyModel[] | undefined;
  degrees: IDegreeReadOnlyModel[] | undefined;
  fellowshipPrograms: IFellowshipProgramReadOnlyModel[] | undefined;
  residencyPrograms: IResidencyProgramReadOnlyModel[] | undefined;
  clinicalLevels: IClinicalLevelReadOnlyModel[] | undefined;
  documentTypes: IDocumentTypeReadOnlyModel[] | undefined;
}

export const PICKLISTS_STATE_TOKEN = new StateToken<IPicklist>('picklists');

@State<IPicklist>({
  name: PICKLISTS_STATE_TOKEN,
  defaults: {
    countries: undefined,
    ethnicities: undefined,
    genders: undefined,
    languages: undefined,
    races: undefined,
    states: [],
    statesMap: {},
    defaultStates: undefined,
    accreditedInstitutions: undefined,
    trainingTypes: undefined,
    graduateProfiles: undefined,
    degrees: undefined,
    fellowshipPrograms: undefined,
    residencyPrograms: undefined,
    clinicalLevels: undefined,
    clinicalActivities: undefined,
    certificateTypes: undefined,
    documentTypes: undefined,
    licenseTypes: undefined,
    practiceTypes: undefined,
    organizationTypes: undefined,
    appointmentTypes: undefined,
    jcahoOrganizations: undefined,
    primaryPractices: undefined,
    scoringSessions: undefined,
    fellowshipTypes: undefined,
  },
})
@Injectable()
export class PicklistsState {
  constructor(
    private _store: Store,
    private picklistsService: PicklistsService,
    private globalDialogService: GlobalDialogService
  ) {}

  @Action(GetCountryList)
  getCountryList(
    ctx: StateContext<IPicklist>
  ): Observable<ICountryReadOnlyModel[] | undefined> {
    if (ctx.getState()?.countries) {
      return of(ctx.getState()?.countries);
    }
    return this.picklistsService.retrieveCountryReadOnly_GetAll().pipe(
      tap((countries: ICountryReadOnlyModel[]) => {
        ctx.patchState({
          countries,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store', error);
        return of(error);
      })
    );
  }

  @Action(GetEthnicityList)
  getEthnicityList(
    ctx: StateContext<IPicklist>
  ): Observable<IEthnicityReadOnlyModel[] | undefined> {
    if (ctx.getState()?.ethnicities) {
      return of(ctx.getState()?.ethnicities);
    }
    return this.picklistsService.retrieveEthnicityReadOnly_GetAll().pipe(
      tap((ethnicities: IEthnicityReadOnlyModel[]) => {
        ctx.patchState({
          ethnicities,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store: Ethnicities', error);
        return of(error);
      })
    );
  }

  @Action(GetGenderList)
  getGenderList(
    ctx: StateContext<IPicklist>
  ): Observable<IPickListItem[] | undefined> {
    if (ctx.getState()?.genders) {
      return of(ctx.getState()?.genders);
    }
    return this.picklistsService.retrieveGenderReadOnly_GetAll().pipe(
      tap((genders: IGenderReadOnlyModel[]) => {
        const transGenders = [] as IPickListItem[];
        genders.forEach((gender) => {
          transGenders.push({
            itemValue: gender.itemValue?.toString(),
            itemDescription: gender.itemDescription,
          });
        });

        ctx.patchState({
          genders: transGenders,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store: Genders', error);
        return of(error);
      })
    );
  }

  @Action(GetLanguageList)
  getLanguageList(
    ctx: StateContext<IPicklist>
  ): Observable<IPickListItem[] | undefined> {
    if (ctx.getState()?.languages) {
      return of(ctx.getState()?.languages);
    }
    return this.picklistsService.retrieveLanguageReadOnly_GetAll().pipe(
      tap((languages: ILanguageReadOnlyModel[]) => {
        const transLanguages = [] as IPickListItem[];
        languages.forEach((language) => {
          transLanguages.push({
            itemValue: language.itemValue?.toString(),
            itemDescription: language.itemDescription,
          });
        });
        ctx.patchState({
          languages: transLanguages,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store: Languages', error);
        return of(error);
      })
    );
  }

  @Action(GetRaceList)
  getRaceList(
    ctx: StateContext<IPicklist>
  ): Observable<IRaceReadOnlyModel[] | undefined> {
    if (ctx.getState()?.races) {
      return of(ctx.getState()?.races);
    }
    return this.picklistsService.retrieveRaceReadOnly_GetAll().pipe(
      tap((races: IRaceReadOnlyModel[]) => {
        ctx.patchState({
          races,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store: Races', error);
        return of(error);
      })
    );
  }

  @Action(GetStateList)
  getStateList(
    ctx: StateContext<IPicklist>,
    payload?: { countryCode: string }
  ): Observable<IStateReadOnlyModel[] | undefined> {
    const statesMap = ctx.getState()?.statesMap
      ? ctx.getState()?.statesMap
      : {};
    if (payload?.countryCode && statesMap[payload.countryCode]) {
      const states: IStateReadOnlyModel[] | undefined =
        statesMap[payload.countryCode];
      ctx.patchState({
        states,
      });
      return of(ctx.getState().states);
    }
    if (payload?.countryCode) {
      return this.picklistsService
        .retrieveStateReadOnly_GetByCountry(payload.countryCode)
        .pipe(
          tap((states: IStateReadOnlyModel[]) => {
            const newStatesMap = { ...statesMap };
            newStatesMap[payload.countryCode] = states;
            ctx.patchState({
              states,
              statesMap: newStatesMap,
            });
          }),
          catchError((error) => {
            console.error('------- In Picklists Store: States', error);
            return of(error);
          })
        );
    } else {
      ctx.patchState({
        states: [],
      });
      return of(ctx.getState().states);
    }
  }

  @Action(GetAccreditedProgramInstitutionsList)
  getAccreditedProgramInstitutionsList(
    ctx: StateContext<IPicklist>
  ): Observable<IAccreditedProgramInstitutionReadOnlyModel[] | undefined> {
    if (ctx.getState()?.accreditedInstitutions) {
      return of(ctx.getState()?.accreditedInstitutions);
    }
    return this.picklistsService
      .retrieveAccreditedProgramInstitutionReadOnly_GetAll()
      .pipe(
        tap((insitutions: IAccreditedProgramInstitutionReadOnlyModel[]) => {
          ctx.patchState({
            accreditedInstitutions: insitutions,
          });
        }),
        catchError((error) => {
          console.error(
            '------- In Picklists Store: Accredited Institutions',
            error
          );
          return of(error);
        })
      );
  }

  @Action(GetTrainingTypeList)
  getTrainingTypeList(
    ctx: StateContext<IPicklist>
  ): Observable<ITrainingTypeReadOnlyModel[] | undefined> {
    if (ctx.getState()?.trainingTypes) {
      return of(ctx.getState()?.trainingTypes);
    }
    return this.picklistsService.retrieveTrainingTypeReadOnly_GetAll().pipe(
      tap((trainingTypes: ITrainingTypeReadOnlyModel[]) => {
        ctx.patchState({
          trainingTypes,
        });
      }),
      catchError((error) => {
        console.error(
          '------- In Picklists Store: Accredited Institutions',
          error
        );
        return of(error);
      })
    );
  }

  @Action(GetGraduateProfiles)
  getGraduateProfiles(
    ctx: StateContext<IPicklist>
  ): Observable<IGraduateProfileReadOnlyModel[] | undefined> {
    if (ctx.getState()?.graduateProfiles) {
      return of(ctx.getState()?.graduateProfiles);
    }
    return this.picklistsService.retrieveGraduateProfileReadOnly_GetAll().pipe(
      tap((graduateProfiles: IGraduateProfileReadOnlyModel[]) => {
        ctx.patchState({
          graduateProfiles,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store: States', error);
        return of(error);
      })
    );
  }

  @Action(GetClinicalLevelList)
  getClinicalLevelList(
    ctx: StateContext<IPicklist>
  ): Observable<IClinicalLevelReadOnlyModel[] | undefined> {
    if (ctx.getState()?.clinicalLevels) {
      return of(ctx.getState()?.clinicalLevels);
    }
    return this.picklistsService.retrieveClinicalLevelReadOnly_GetAll().pipe(
      tap((clinicalLevels: IClinicalLevelReadOnlyModel[]) => {
        ctx.patchState({
          clinicalLevels,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store: Clinical Levels', error);
        return of(error);
      })
    );
  }

  @Action(GetDegrees)
  getDegrees(
    ctx: StateContext<IPicklist>
  ): Observable<IDegreeReadOnlyModel[] | undefined> {
    if (ctx.getState()?.degrees) {
      return of(ctx.getState()?.degrees);
    }
    return this.picklistsService.retrieveDegreeReadOnly_GetAll().pipe(
      tap((degrees: IDegreeReadOnlyModel[]) => {
        ctx.patchState({
          degrees,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store: States', error);
        return of(error);
      })
    );
  }

  @Action(GetFellowshipTypes)
  getFellowshipTypes(
    ctx: StateContext<IPicklist>
  ): Observable<IFellowshipTypeReadOnlyModel[] | undefined> {
    if (ctx.getState()?.fellowshipTypes) {
      return of(ctx.getState()?.fellowshipTypes);
    }
    return this.picklistsService.retrieveFellowshipTypeReadOnly_Get().pipe(
      tap((fellowshipTypes: IFellowshipTypeReadOnlyModel[]) => {
        ctx.patchState({
          fellowshipTypes,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store: States', error);
        return of(error);
      })
    );
  }

  @Action(GetFellowshipPrograms)
  getFellowshipPrograms(
    ctx: StateContext<IPicklist>,
    payload: { fellowshipType: string }
  ): Observable<IFellowshipProgramReadOnlyModel[] | undefined> {
    this.globalDialogService.showLoading();
    if (!payload.fellowshipType) {
      return of([]);
    }
    return this.picklistsService
      .retrieveFellowshipProgramReadOnly_GetAll(payload.fellowshipType)
      .pipe(
        tap((fellowshipPrograms: IFellowshipProgramReadOnlyModel[]) => {
          ctx.patchState({
            fellowshipPrograms,
          });
          this.globalDialogService.closeOpenDialog();
        }),
        catchError((error) => {
          console.error('------- In Picklists Store: States', error);
          this.globalDialogService.closeOpenDialog();
          return of(error);
        })
      );
  }

  @Action(GetResidencyPrograms)
  getResidencyPrograms(
    ctx: StateContext<IPicklist>
  ): Observable<IResidencyProgramReadOnlyModel[] | undefined> {
    if (ctx.getState()?.residencyPrograms) {
      return of(ctx.getState()?.residencyPrograms);
    }
    return this.picklistsService.retrieveResidencyProgramReadOnly_GetAll().pipe(
      tap((residencyPrograms: IResidencyProgramReadOnlyModel[]) => {
        ctx.patchState({
          residencyPrograms,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store: States', error);
        return of(error);
      })
    );
  }

  @Action(GetClinicalActivityList)
  getClinicalActivityList(
    ctx: StateContext<IPicklist>
  ): Observable<IClinicalActivityReadOnlyModel[] | undefined> {
    if (ctx.getState()?.clinicalActivities) {
      return of(ctx.getState()?.clinicalActivities);
    }
    return this.picklistsService.retrieveClinicalActivityReadOnly_GetAll().pipe(
      tap((clinicalActivities: IClinicalActivityReadOnlyModel[]) => {
        ctx.patchState({
          clinicalActivities,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store: Clinical Activities', error);
        return of(error);
      })
    );
  }

  @Action(GetCertificateTypes)
  getCertificateTypes(
    ctx: StateContext<IPicklist>
  ): Observable<ICertificateTypeReadOnlyModel[] | undefined> {
    if (ctx.getState()?.certificateTypes) {
      return of(ctx.getState()?.certificateTypes);
    }
    return this.picklistsService.retrieveCertificateTypeReadOnly_GetAll().pipe(
      tap((certificateTypes: ICertificateTypeReadOnlyModel[]) => {
        ctx.patchState({
          certificateTypes,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store: Certificate Types', error);
        return of(error);
      })
    );
  }

  @Action(GetLicenseTypeList)
  getLicenseTypeList(
    ctx: StateContext<IPicklist>
  ): Observable<IPickListItemNumber[] | undefined> {
    if (ctx.getState()?.licenseTypes) {
      return of(ctx.getState()?.licenseTypes);
    }
    return this.picklistsService.retrieveLicenseTypeReadOnly_GetAll().pipe(
      tap((licenseTypes: ILicenseTypeReadOnlyModel[]) => {
        const licenseTypesList = [] as IPickListItemNumber[];
        licenseTypes.forEach((type) => {
          licenseTypesList.push({
            itemValue: type.id,
            itemDescription: type.name,
          });
        });

        ctx.patchState({
          licenseTypes: licenseTypesList,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store: License Types', error);
        return of(error);
      })
    );
  }

  @Action(GetPracticeTypeList)
  getPracticeTypeList(
    ctx: StateContext<IPicklist>
  ): Observable<IPickListItemNumber[] | undefined> {
    if (ctx.getState()?.practiceTypes) {
      return of(ctx.getState()?.practiceTypes);
    }
    return this.picklistsService.retrievePracticeTypeReadOnly_GetAll().pipe(
      tap((practiceTypes: IPracticeTypeReadOnlyModel[]) => {
        const practiceTypesList = [] as IPickListItemNumber[];
        practiceTypes.forEach((type) => {
          practiceTypesList.push({
            itemValue: type.id,
            itemDescription: type.name,
          });
        });

        ctx.patchState({
          practiceTypes: practiceTypesList,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store: Practice Types', error);
        return of(error);
      })
    );
  }

  @Action(GetOrganizationTypeList)
  getOrganizationTypeList(
    ctx: StateContext<IPicklist>
  ): Observable<IPickListItemNumber[] | undefined> {
    if (ctx.getState()?.organizationTypes) {
      return of(ctx.getState()?.organizationTypes);
    }
    return this.picklistsService.retrieveOrganizationTypeReadOnly_GetAll().pipe(
      tap((orgTypes: IOrganizationTypeReadOnlyModel[]) => {
        const orgTypesList = [] as IPickListItemNumber[];
        orgTypes.forEach((type) => {
          orgTypesList.push({
            itemValue: type.id,
            itemDescription: type.type,
          });
        });

        ctx.patchState({
          organizationTypes: orgTypesList,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store: Organization Types', error);
        return of(error);
      })
    );
  }

  @Action(GetAppointmentTypeList)
  getAppointmentTypeList(
    ctx: StateContext<IPicklist>
  ): Observable<IPickListItemNumber[] | undefined> {
    if (ctx.getState()?.appointmentTypes) {
      return of(ctx.getState()?.appointmentTypes);
    }
    return this.picklistsService.retrieveAppointmentTypeReadOnly_GetAll().pipe(
      tap((appointmentTypes: IAppointmentTypeReadOnlyModel[]) => {
        const appointmentTypesList = [] as IPickListItemNumber[];
        appointmentTypes.forEach((type) => {
          appointmentTypesList.push({
            itemValue: type.id,
            itemDescription: type.name,
          });
        });

        ctx.patchState({
          appointmentTypes: appointmentTypesList,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store: Appointment Types', error);
        return of(error);
      })
    );
  }

  @Action(GetJcahoOrganizationList)
  getJcahoOrganizationList(
    ctx: StateContext<IPicklist>
  ): Observable<IPickListItemNumber[] | undefined> {
    if (ctx.getState()?.jcahoOrganizations) {
      return of(ctx.getState()?.jcahoOrganizations);
    }
    return this.picklistsService
      .retrieveJcahoOrganizationReadOnly_GetAll()
      .pipe(
        tap((jcahoOrganizations: IJcahoOrganizationReadOnlyModel[]) => {
          const jcahoOrganizationList = [] as IPickListItemNumber[];
          jcahoOrganizations.forEach((type) => {
            jcahoOrganizationList.push({
              itemValue: type.organizationId,
              itemDescription: type.organizationName,
            });
          });

          ctx.patchState({
            jcahoOrganizations: jcahoOrganizationList,
          });
        }),
        catchError((error) => {
          console.error('------- In Picklists Store: Appointment Types', error);
          return of(error);
        })
      );
  }

  @Action(GetPrimaryPracticeList)
  getPrimaryPracticeList(
    ctx: StateContext<IPicklist>
  ): Observable<IPickListItemNumber[] | undefined> {
    if (ctx.getState()?.primaryPractices) {
      return of(ctx.getState()?.primaryPractices);
    }
    return this.picklistsService.retrievePrimaryPracticeReadOnly_GetAll().pipe(
      tap((primaryPractices: IPrimaryPracticeReadOnlyModel[]) => {
        const primaryPracticeList = [] as IPickListItemNumber[];
        primaryPractices.forEach((type) => {
          primaryPracticeList.push({
            itemValue: type.id,
            itemDescription: type.practice,
          });
        });

        ctx.patchState({
          primaryPractices: primaryPracticeList,
        });
      }),
      catchError((error) => {
        console.error('------- In Picklists Store: Primary Practices', error);
        return of(error);
      })
    );
  }

  @Action(GetScoringSessionList)
  getScoringSessionList(
    ctx: StateContext<IPicklist>
    //payload: { date: string }
  ): Observable<IPickListItemNumber[] | undefined> {
    //const examHeaderId = payload.id;
    const currentDate = new Date().toISOString();
    // removed because we don't want to save this value in the store because it will change based on passed in id
    // if (ctx.getState()?.primaryPractices) {
    //   return of(ctx.getState()?.primaryPractices);
    // }
    return this.picklistsService
      .retrieveScoringSessionReadOnly_GetByKeys(currentDate)
      .pipe(
        tap((scoringSessions: IScoringSessionReadOnlyModel[]) => {
          ctx.patchState({
            scoringSessions: scoringSessions,
          });
        }),
        catchError((error) => {
          console.error('------- In Picklists Store: Scoring Sessions', error);
          return of(error);
        })
      );
  }

  @Action(GetPicklists)
  getPicklists(
    ctx: StateContext<IPicklist>,
    payload?: { countryCode: string; fellowshipType: string }
  ): Observable<IPicklist> {
    const joins = [
      this.getCountryList(ctx).pipe(catchError((error) => of(error))),
      this.getEthnicityList(ctx).pipe(catchError((error) => of(error))),
      this.getGenderList(ctx).pipe(catchError((error) => of(error))),
      this.getLanguageList(ctx).pipe(catchError((error) => of(error))),
      this.getRaceList(ctx).pipe(catchError((error) => of(error))),
      this.getAccreditedProgramInstitutionsList(ctx).pipe(
        catchError((error) => of(error))
      ),
      this.getDegrees(ctx).pipe(catchError((error) => of(error))),
      this.getTrainingTypeList(ctx).pipe(catchError((error) => of(error))),

      this.getResidencyPrograms(ctx).pipe(catchError((error) => of(error))),
      this.getCertificateTypes(ctx).pipe(catchError((error) => of(error))),
      this.getLicenseTypeList(ctx).pipe(catchError((error) => of(error))),
      this.getPracticeTypeList(ctx).pipe(catchError((error) => of(error))),
      this.getOrganizationTypeList(ctx).pipe(catchError((error) => of(error))),
      this.getAppointmentTypeList(ctx).pipe(catchError((error) => of(error))),
      this.getJcahoOrganizationList(ctx).pipe(catchError((error) => of(error))),
      this.getPrimaryPracticeList(ctx).pipe(catchError((error) => of(error))),
      this.getClinicalLevelList(ctx).pipe(catchError((error) => of(error))),
      this.getClinicalActivityList(ctx).pipe(catchError((error) => of(error))),
    ];

    if (payload?.countryCode) {
      joins.push(
        this.getStateList(ctx, { countryCode: payload.countryCode }).pipe(
          catchError((error) => of(error))
        )
      );
    }

    if (payload?.fellowshipType) {
      joins.push(
        this.getFellowshipPrograms(ctx, {
          fellowshipType: payload.fellowshipType,
        }).pipe(catchError((error) => of(error)))
      );
    }

    return forkJoin(joins).pipe(
      map((picklists: IPicklist[]) => {
        return of(ctx.getState());
      }),
      share(),
      catchError((error) => {
        console.error('------- In Picklists Store', error);
        return of(error);
      })
    );
  }
}
