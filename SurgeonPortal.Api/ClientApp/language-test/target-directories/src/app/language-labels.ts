/****************************************************************************************
This file is auto generated. Any changes to this file will be overwritten
****************************************************************************************/
import { TranslateService } from '@ngx-translate/core';
import { Injectable } from '@angular/core';

export const supportedLanguages: string[] = ['en-US', 'fr-CA', 'en-CA'];

export interface ICME_ITEMIZED_CME {
  TITLE: string;
}
export interface ICME_WAIVERS {
  SUBTITLE: string;
  TITLE: string;
}
export interface ICME {
  HELPTEXT: string;
  ITEMIZED_CME: ICME_ITEMIZED_CME;
  SUBTITLE: string;
  TITLE: string;
  TOOLTIP: string;
  WAIVERS: ICME_WAIVERS;
}
export interface IDASHBOARD_ACTION_CARDS {
  APPLY_BTN: string;
  APPLY_SUBTITLE: string;
  APPLY_TITLE: string;
  CCR_BTN: string;
  CCR_SUBTITLE: string;
  CCR_TITLE: string;
  CME_BTN: string;
  CME_SUBTITLE: string;
  CME_TITLE: string;
  GME_BTN: string;
  GME_SUBTITLE: string;
  GME_TITLE: string;
  REGISTER_BTN: string;
  REGISTER_SUBTITLE: string;
  REGISTER_TITLE: string;
}
export interface IDASHBOARD_HIGHLIGHT_CARDS {
  DOCUMENTS_BTN: string;
  DOCUMENTS_SUBTITLE: string;
  DOCUMENTS_TITLE: string;
  EXAMREGISTRATION_TITLE: string;
  UPCOMINGEXAMS_SUBTITLE: string;
  UPCOMINGEXAMS_TITLE: string;
}
export interface IDASHBOARD {
  ACTION_CARDS: IDASHBOARD_ACTION_CARDS;
  HIGHLIGHT_CARDS: IDASHBOARD_HIGHLIGHT_CARDS;
  LASTLOGIN: string;
  STATUS: string;
  SUBTITLE: string;
  TITLE: string;
}
export interface IDOCUMENTS {
  SUBTITLE: string;
  TITLE: string;
}
export interface IEXAMSCORING_DASHBOARD {
  AGENDA_BTN: string;
  AGENDA_SUBTITLE: string;
  AGENDA_TITLE: string;
  CERTIFYING_NORESULTS: string;
  CERTIFYING_TITLE: string;
  CONFLICTS_BTN: string;
  CONFLICTS_SUBTITLE: string;
  CONFLICTS_TITLE: string;
  DELIVER_BTN: string;
  DELIVER_SUBTITLE: string;
  DELIVER_TITLE: string;
  ROSTER_BTN: string;
  ROSTER_SUBTITLE: string;
  ROSTER_TITLE: string;
  SCORE_BTN: string;
  SCORE_SUBTITLE: string;
  SCORE_TITLE: string;
  SUBTITLE: string;
}
export interface IEXAMSCORING_EXAMINATION {
  CANDIDATE: string;
  INSTRUCTIONS: string;
  LIST_DATE: string;
  LIST_TITLE: string;
  SUBTITLE: string;
  TIME: string;
  TITLE: string;
}
export interface IEXAMSCORING_EXAMROSTERS {
  TITLE: string;
}
export interface IEXAMSCORING_EXAMSCORES {
  SUBTITLE: string;
  TITLE: string;
}
export interface IEXAMSCORING {
  DASHBOARD: IEXAMSCORING_DASHBOARD;
  DEFAULT_EXAM: string;
  EXAMINATION: IEXAMSCORING_EXAMINATION;
  EXAMROSTERS: IEXAMSCORING_EXAMROSTERS;
  EXAMSCORES: IEXAMSCORING_EXAMSCORES;
}
export interface IGME_ITEMIZED_GME {
  SUBTITLE: string;
  TITLE: string;
}
export interface IGME_ROTATIONS {
  SUBTITLE: string;
  TITLE: string;
}
export interface IGME_SUMMARY {
  SUBTITLE: string;
  TITLE: string;
}
export interface IGME {
  ITEMIZED_GME: IGME_ITEMIZED_GME;
  ROTATIONS: IGME_ROTATIONS;
  SUMMARY: IGME_SUMMARY;
  TITLE: string;
}
export interface ILOGIN {
  PASSWORD: string;
  PASSWORD_ERROR: string;
  SUBMIT_BTN: string;
  SUBTITLE: string;
  TITLE: string;
  USERNAME: string;
  USERNAME_ERROR: string;
}
export interface IMEDICAL_TRAINING_ADVANCED_TRAINING {
  TITLE: string;
}
export interface IMEDICAL_TRAINING_CERTIFICATES {
  OTHER_CERTS_TITLE: string;
  TITLE: string;
}
export interface IMEDICAL_TRAINING_FELLOWSHIP {
  SUBTITLE: string;
  TITLE: string;
}
export interface IMEDICAL_TRAINING_MEDICAL_SCHOOL {
  SUBTITLE: string;
  TITLE: string;
}
export interface IMEDICAL_TRAINING_RESIDENCY {
  SUBTITLE: string;
  TITLE: string;
}
export interface IMEDICAL_TRAINING {
  ADVANCED_TRAINING: IMEDICAL_TRAINING_ADVANCED_TRAINING;
  CERTIFICATES: IMEDICAL_TRAINING_CERTIFICATES;
  FELLOWSHIP: IMEDICAL_TRAINING_FELLOWSHIP;
  MEDICAL_SCHOOL: IMEDICAL_TRAINING_MEDICAL_SCHOOL;
  RESIDENCY: IMEDICAL_TRAINING_RESIDENCY;
  TITLE: string;
}
export interface Imeta {
  locale: string;
}
export interface IMYACCOUNT {
  SUBTITLE: string;
  TITLE: string;
}
export interface IPROFESSIONAL_STANDING_APPOINTMENTS {
  ALL_APPOINTMENTS_TITLE: string;
  SUBTITLE: string;
  TITLE: string;
}
export interface IPROFESSIONAL_STANDING_MEDICAL_LICENSE {
  SUBTITLE: string;
  TITLE: string;
}
export interface IPROFESSIONAL_STANDING_SANCTIONS {
  TITLE: string;
}
export interface IPROFESSIONAL_STANDING {
  APPOINTMENTS: IPROFESSIONAL_STANDING_APPOINTMENTS;
  MEDICAL_LICENSE: IPROFESSIONAL_STANDING_MEDICAL_LICENSE;
  SANCTIONS: IPROFESSIONAL_STANDING_SANCTIONS;
  TITLE: string;
}
export interface IPROFILE {
  SUBTITLE: string;
  TITLE: string;
}
export interface ISHELL {
  ADD_BTN: string;
  CANCEL_BTN: string;
  CLOSE_BTN: string;
  CONTINUE_BTN: string;
  COPYRIGHT: string;
  DELETE_BTN: string;
  EDIT_BTN: string;
  NO_BTN: string;
  SAVE_BTN: string;
  SUBMIT_BTN: string;
  YES_BTN: string;
}
export interface ISIDENAV_APPLY_REGISTER {
  MAIN: string;
  REGISTRATION: string;
  REQUIREMENTS: string;
}
export interface ISIDENAV_EXAM_SCORING {
  CASE_ROSTER: string;
  EXAMINATION: string;
  MAIN: string;
  SCORES: string;
}
export interface ISIDENAV {
  APPLY_REGISTER: ISIDENAV_APPLY_REGISTER;
  CME: string;
  CONTINUOUS_CERTIFICATION: string;
  DASHBOARD: string;
  DOCUMENTS: string;
  EXAM_HISTORY: string;
  EXAM_SCORING: ISIDENAV_EXAM_SCORING;
  GME: string;
  MEDICAL_TRAINING: string;
  MYACCOUNT: string;
  PAYMENT_HISTORY: string;
  PROFESSIONAL_STANDING: string;
  PROFILE: string;
}
export interface ILanguageLabelsNGX {
  CME: ICME;
  DASHBOARD: IDASHBOARD;
  DOCUMENTS: IDOCUMENTS;
  EXAMSCORING: IEXAMSCORING;
  GME: IGME;
  LOGIN: ILOGIN;
  MEDICAL_TRAINING: IMEDICAL_TRAINING;
  meta: Imeta;
  MYACCOUNT: IMYACCOUNT;
  PROFESSIONAL_STANDING: IPROFESSIONAL_STANDING;
  PROFILE: IPROFILE;
  SHELL: ISHELL;
  SIDENAV: ISIDENAV;
}

export class LanguageLabelsNGX implements ILanguageLabelsNGX {
  CME = {
    HELPTEXT: 'CME.HELPTEXT',
    ITEMIZED_CME: {
      TITLE: 'CME.ITEMIZED_CME.TITLE',
    },
    SUBTITLE: 'CME.SUBTITLE',
    TITLE: 'CME.TITLE',
    TOOLTIP: 'CME.TOOLTIP',
    WAIVERS: {
      SUBTITLE: 'CME.WAIVERS.SUBTITLE',
      TITLE: 'CME.WAIVERS.TITLE',
    },
  };
  DASHBOARD = {
    ACTION_CARDS: {
      APPLY_BTN: 'DASHBOARD.ACTION_CARDS.APPLY_BTN',
      APPLY_SUBTITLE: 'DASHBOARD.ACTION_CARDS.APPLY_SUBTITLE',
      APPLY_TITLE: 'DASHBOARD.ACTION_CARDS.APPLY_TITLE',
      CCR_BTN: 'DASHBOARD.ACTION_CARDS.CCR_BTN',
      CCR_SUBTITLE: 'DASHBOARD.ACTION_CARDS.CCR_SUBTITLE',
      CCR_TITLE: 'DASHBOARD.ACTION_CARDS.CCR_TITLE',
      CME_BTN: 'DASHBOARD.ACTION_CARDS.CME_BTN',
      CME_SUBTITLE: 'DASHBOARD.ACTION_CARDS.CME_SUBTITLE',
      CME_TITLE: 'DASHBOARD.ACTION_CARDS.CME_TITLE',
      GME_BTN: 'DASHBOARD.ACTION_CARDS.GME_BTN',
      GME_SUBTITLE: 'DASHBOARD.ACTION_CARDS.GME_SUBTITLE',
      GME_TITLE: 'DASHBOARD.ACTION_CARDS.GME_TITLE',
      REGISTER_BTN: 'DASHBOARD.ACTION_CARDS.REGISTER_BTN',
      REGISTER_SUBTITLE: 'DASHBOARD.ACTION_CARDS.REGISTER_SUBTITLE',
      REGISTER_TITLE: 'DASHBOARD.ACTION_CARDS.REGISTER_TITLE',
    },
    HIGHLIGHT_CARDS: {
      DOCUMENTS_BTN: 'DASHBOARD.HIGHLIGHT_CARDS.DOCUMENTS_BTN',
      DOCUMENTS_SUBTITLE: 'DASHBOARD.HIGHLIGHT_CARDS.DOCUMENTS_SUBTITLE',
      DOCUMENTS_TITLE: 'DASHBOARD.HIGHLIGHT_CARDS.DOCUMENTS_TITLE',
      EXAMREGISTRATION_TITLE:
        'DASHBOARD.HIGHLIGHT_CARDS.EXAMREGISTRATION_TITLE',
      UPCOMINGEXAMS_SUBTITLE:
        'DASHBOARD.HIGHLIGHT_CARDS.UPCOMINGEXAMS_SUBTITLE',
      UPCOMINGEXAMS_TITLE: 'DASHBOARD.HIGHLIGHT_CARDS.UPCOMINGEXAMS_TITLE',
    },
    LASTLOGIN: 'DASHBOARD.LASTLOGIN',
    STATUS: 'DASHBOARD.STATUS',
    SUBTITLE: 'DASHBOARD.SUBTITLE',
    TITLE: 'DASHBOARD.TITLE',
  };
  DOCUMENTS = {
    SUBTITLE: 'DOCUMENTS.SUBTITLE',
    TITLE: 'DOCUMENTS.TITLE',
  };
  EXAMSCORING = {
    DASHBOARD: {
      AGENDA_BTN: 'EXAMSCORING.DASHBOARD.AGENDA_BTN',
      AGENDA_SUBTITLE: 'EXAMSCORING.DASHBOARD.AGENDA_SUBTITLE',
      AGENDA_TITLE: 'EXAMSCORING.DASHBOARD.AGENDA_TITLE',
      CERTIFYING_NORESULTS: 'EXAMSCORING.DASHBOARD.CERTIFYING_NORESULTS',
      CERTIFYING_TITLE: 'EXAMSCORING.DASHBOARD.CERTIFYING_TITLE',
      CONFLICTS_BTN: 'EXAMSCORING.DASHBOARD.CONFLICTS_BTN',
      CONFLICTS_SUBTITLE: 'EXAMSCORING.DASHBOARD.CONFLICTS_SUBTITLE',
      CONFLICTS_TITLE: 'EXAMSCORING.DASHBOARD.CONFLICTS_TITLE',
      DELIVER_BTN: 'EXAMSCORING.DASHBOARD.DELIVER_BTN',
      DELIVER_SUBTITLE: 'EXAMSCORING.DASHBOARD.DELIVER_SUBTITLE',
      DELIVER_TITLE: 'EXAMSCORING.DASHBOARD.DELIVER_TITLE',
      ROSTER_BTN: 'EXAMSCORING.DASHBOARD.ROSTER_BTN',
      ROSTER_SUBTITLE: 'EXAMSCORING.DASHBOARD.ROSTER_SUBTITLE',
      ROSTER_TITLE: 'EXAMSCORING.DASHBOARD.ROSTER_TITLE',
      SCORE_BTN: 'EXAMSCORING.DASHBOARD.SCORE_BTN',
      SCORE_SUBTITLE: 'EXAMSCORING.DASHBOARD.SCORE_SUBTITLE',
      SCORE_TITLE: 'EXAMSCORING.DASHBOARD.SCORE_TITLE',
      SUBTITLE: 'EXAMSCORING.DASHBOARD.SUBTITLE',
    },
    DEFAULT_EXAM: 'EXAMSCORING.DEFAULT_EXAM',
    EXAMINATION: {
      CANDIDATE: 'EXAMSCORING.EXAMINATION.CANDIDATE',
      INSTRUCTIONS: 'EXAMSCORING.EXAMINATION.INSTRUCTIONS',
      LIST_DATE: 'EXAMSCORING.EXAMINATION.LIST_DATE',
      LIST_TITLE: 'EXAMSCORING.EXAMINATION.LIST_TITLE',
      SUBTITLE: 'EXAMSCORING.EXAMINATION.SUBTITLE',
      TIME: 'EXAMSCORING.EXAMINATION.TIME',
      TITLE: 'EXAMSCORING.EXAMINATION.TITLE',
    },
    EXAMROSTERS: {
      TITLE: 'EXAMSCORING.EXAMROSTERS.TITLE',
    },
    EXAMSCORES: {
      SUBTITLE: 'EXAMSCORING.EXAMSCORES.SUBTITLE',
      TITLE: 'EXAMSCORING.EXAMSCORES.TITLE',
    },
  };
  GME = {
    ITEMIZED_GME: {
      SUBTITLE: 'GME.ITEMIZED_GME.SUBTITLE',
      TITLE: 'GME.ITEMIZED_GME.TITLE',
    },
    ROTATIONS: {
      SUBTITLE: 'GME.ROTATIONS.SUBTITLE',
      TITLE: 'GME.ROTATIONS.TITLE',
    },
    SUMMARY: {
      SUBTITLE: 'GME.SUMMARY.SUBTITLE',
      TITLE: 'GME.SUMMARY.TITLE',
    },
    TITLE: 'GME.TITLE',
  };
  LOGIN = {
    PASSWORD: 'LOGIN.PASSWORD',
    PASSWORD_ERROR: 'LOGIN.PASSWORD_ERROR',
    SUBMIT_BTN: 'LOGIN.SUBMIT_BTN',
    SUBTITLE: 'LOGIN.SUBTITLE',
    TITLE: 'LOGIN.TITLE',
    USERNAME: 'LOGIN.USERNAME',
    USERNAME_ERROR: 'LOGIN.USERNAME_ERROR',
  };
  MEDICAL_TRAINING = {
    ADVANCED_TRAINING: {
      TITLE: 'MEDICAL_TRAINING.ADVANCED_TRAINING.TITLE',
    },
    CERTIFICATES: {
      OTHER_CERTS_TITLE: 'MEDICAL_TRAINING.CERTIFICATES.OTHER_CERTS_TITLE',
      TITLE: 'MEDICAL_TRAINING.CERTIFICATES.TITLE',
    },
    FELLOWSHIP: {
      SUBTITLE: 'MEDICAL_TRAINING.FELLOWSHIP.SUBTITLE',
      TITLE: 'MEDICAL_TRAINING.FELLOWSHIP.TITLE',
    },
    MEDICAL_SCHOOL: {
      SUBTITLE: 'MEDICAL_TRAINING.MEDICAL_SCHOOL.SUBTITLE',
      TITLE: 'MEDICAL_TRAINING.MEDICAL_SCHOOL.TITLE',
    },
    RESIDENCY: {
      SUBTITLE: 'MEDICAL_TRAINING.RESIDENCY.SUBTITLE',
      TITLE: 'MEDICAL_TRAINING.RESIDENCY.TITLE',
    },
    TITLE: 'MEDICAL_TRAINING.TITLE',
  };
  meta = {
    locale: 'meta.locale',
  };
  MYACCOUNT = {
    SUBTITLE: 'MYACCOUNT.SUBTITLE',
    TITLE: 'MYACCOUNT.TITLE',
  };
  PROFESSIONAL_STANDING = {
    APPOINTMENTS: {
      ALL_APPOINTMENTS_TITLE:
        'PROFESSIONAL_STANDING.APPOINTMENTS.ALL_APPOINTMENTS_TITLE',
      SUBTITLE: 'PROFESSIONAL_STANDING.APPOINTMENTS.SUBTITLE',
      TITLE: 'PROFESSIONAL_STANDING.APPOINTMENTS.TITLE',
    },
    MEDICAL_LICENSE: {
      SUBTITLE: 'PROFESSIONAL_STANDING.MEDICAL_LICENSE.SUBTITLE',
      TITLE: 'PROFESSIONAL_STANDING.MEDICAL_LICENSE.TITLE',
    },
    SANCTIONS: {
      TITLE: 'PROFESSIONAL_STANDING.SANCTIONS.TITLE',
    },
    TITLE: 'PROFESSIONAL_STANDING.TITLE',
  };
  PROFILE = {
    SUBTITLE: 'PROFILE.SUBTITLE',
    TITLE: 'PROFILE.TITLE',
  };
  SHELL = {
    ADD_BTN: 'SHELL.ADD_BTN',
    CANCEL_BTN: 'SHELL.CANCEL_BTN',
    CLOSE_BTN: 'SHELL.CLOSE_BTN',
    CONTINUE_BTN: 'SHELL.CONTINUE_BTN',
    COPYRIGHT: 'SHELL.COPYRIGHT',
    DELETE_BTN: 'SHELL.DELETE_BTN',
    EDIT_BTN: 'SHELL.EDIT_BTN',
    NO_BTN: 'SHELL.NO_BTN',
    SAVE_BTN: 'SHELL.SAVE_BTN',
    SUBMIT_BTN: 'SHELL.SUBMIT_BTN',
    YES_BTN: 'SHELL.YES_BTN',
  };
  SIDENAV = {
    APPLY_REGISTER: {
      MAIN: 'SIDENAV.APPLY_REGISTER.MAIN',
      REGISTRATION: 'SIDENAV.APPLY_REGISTER.REGISTRATION',
      REQUIREMENTS: 'SIDENAV.APPLY_REGISTER.REQUIREMENTS',
    },
    CME: 'SIDENAV.CME',
    CONTINUOUS_CERTIFICATION: 'SIDENAV.CONTINUOUS_CERTIFICATION',
    DASHBOARD: 'SIDENAV.DASHBOARD',
    DOCUMENTS: 'SIDENAV.DOCUMENTS',
    EXAM_HISTORY: 'SIDENAV.EXAM_HISTORY',
    EXAM_SCORING: {
      CASE_ROSTER: 'SIDENAV.EXAM_SCORING.CASE_ROSTER',
      EXAMINATION: 'SIDENAV.EXAM_SCORING.EXAMINATION',
      MAIN: 'SIDENAV.EXAM_SCORING.MAIN',
      SCORES: 'SIDENAV.EXAM_SCORING.SCORES',
    },
    GME: 'SIDENAV.GME',
    MEDICAL_TRAINING: 'SIDENAV.MEDICAL_TRAINING',
    MYACCOUNT: 'SIDENAV.MYACCOUNT',
    PAYMENT_HISTORY: 'SIDENAV.PAYMENT_HISTORY',
    PROFESSIONAL_STANDING: 'SIDENAV.PROFESSIONAL_STANDING',
    PROFILE: 'SIDENAV.PROFILE',
  };
}

@Injectable()
export class LanguageHelper {
  constructor(private translateService: TranslateService) {}

  setupLanguage(lang?: string | undefined): void {
    this.translateService.addLangs(supportedLanguages);
    this.translateService.setDefaultLang(supportedLanguages[0]);

    let browserLang = this.translateService.getBrowserLang();
    browserLang = browserLang ? browserLang : supportedLanguages[0];
    lang = lang ? lang : browserLang;

    this.translateService.use(
      supportedLanguages.includes(lang) ? lang : supportedLanguages[0]
    );
  }
}
