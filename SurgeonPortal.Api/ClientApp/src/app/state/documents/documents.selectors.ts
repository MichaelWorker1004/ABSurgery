import { createPropertySelectors, Selector } from '@ngxs/store';
import { DocumentsState, IDocuments } from './documents.state';

export class DocumentSelectors {
  static slices = createPropertySelectors<IDocuments>(DocumentsState);

  @Selector([DocumentsState])
  static documents(state: IDocuments): IDocuments | undefined {
    if (state) {
      return {
        documents: state.documents,
      } as IDocuments;
    }
    return;
  }
}
