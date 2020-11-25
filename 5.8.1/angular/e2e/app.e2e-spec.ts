import { EmpAspnetBoilerplateTemplatePage } from './app.po';

describe('EmpAspnetBoilerplate App', function() {
  let page: EmpAspnetBoilerplateTemplatePage;

  beforeEach(() => {
    page = new EmpAspnetBoilerplateTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
