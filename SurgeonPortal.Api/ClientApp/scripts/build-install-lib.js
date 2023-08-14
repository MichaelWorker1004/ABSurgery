import { YTGUtils } from "./utils.js";

// async function packLib() {
//   const timeStamp = new Date().getTime();
//   const updatePackage = await YTGUtils.updatePackageJsonVersion(`15.0.${timeStamp}`);
//   console.log('step 1', updatePackage);
//   const buildLib = await YTGUtils.executeCommand("npm run lib-build");
//   const packLib = await YTGUtils.executeCommand("cd dist/ytg-angular-material && npm pack .");
//   const installed = await YTGUtils.executeCommand(`npm install dist/ytg-angular-material/ytg-angular-material-${updatePackage}.tgz`);
//   return `Library is packedz ${buildLib} AND ${packLib}, ${installed}`;
// }

// packLib().then((message) => {
//   console.log(message);
// });
