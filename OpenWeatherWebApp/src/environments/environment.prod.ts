import {DynamicEnvironment} from '../environments/dynamic-environment';

class Environment extends DynamicEnvironment{
  public production: boolean;
  /**
   *
   */
  constructor() {
    super();
    this.production = false;
  }
}

export const environment = new Environment();
