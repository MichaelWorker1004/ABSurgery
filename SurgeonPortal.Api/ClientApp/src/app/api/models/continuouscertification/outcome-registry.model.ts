
export interface IOutcomeRegistryModel {
    surgeonSpecificRegistry: boolean;
    registryComments: string;
    registeredWithACHQC: boolean;
    registeredWithCESQIP: boolean;
    registeredWithMBSAQIP: boolean;
    registeredWithABA: boolean;
    registeredWithASBS: boolean;
    registeredWithStatewideCollaboratives: boolean;
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
    userConfirmed: boolean;
    userConfirmedDateUtc: string;
    userId: number;
}
