import { YTGUtils } from "./utils.js";

// YTGUtils.deleteDirectory("node_modules");
// TODO: Make a script to copy library from dist/ytg-n to directory root and install it via npm

YTGUtils.copyFileFromTo(
  "dist/ytg-angular/ytg-angular-0.0.1.tgz",
  "ytg-angular-0.0.1.tgz"
);
