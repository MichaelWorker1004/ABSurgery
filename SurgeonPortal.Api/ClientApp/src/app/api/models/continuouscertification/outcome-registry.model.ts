
export interface IOutcomeRegistryModel {
    id: number;
    userId: number;
    surgeonSpecificRegistry: string;
    registryComments: string;
    registeredWithACHQC: boolean;
    registeredWithCESQIP: boolean;
    registeredWithMBSAQIP: boolean;
    registeredWithABA: boolean;
    registeredWithASBS: boolean;
    registeredWithMSQC: boolean;
    registeredWithABMS: boolean;
    registeredWithNCDB: boolean;
    registeredWithRQRS: boolean;
    registeredWithNSQIP: boolean;
    registeredWithNTDB: boolean;
    registeredWithSTS: boolean;
    registeredWithTQIP: boolean;
    registeredWithUNOS: boolean;
    registeredWithNCDR: boolean;
    registeredWithSVS: boolean;
    registeredWithELSO: boolean;
    registeredWithSSR: boolean;
    userConfirmed: boolean;
    userConfirmedDateUtc: string;
    createdByUserId: number;
    createdAtUtc: string;
    lastUpdatedAtUtc: string;
    lastUpdatedByUserId: number;
}
