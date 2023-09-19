import fs, { readFileSync, writeFileSync, existsSync } from "fs";
import * as process from "process";
import util from "util";
import path from "path";
import * as XLSX from "xlsx";
import { Readable } from "stream";
import { fileURLToPath } from "url";

const __dirname = path.dirname(fileURLToPath(import.meta.url));

XLSX.set_fs(fs); // DO NOT FORGET THIS LINE! (required for use with raw data)
XLSX.stream.set_readable(Readable); // DO NOT FORGET THIS LINE! (required for use with raw data)

export class WriteToLocales {
  static locales;
  static readFromExcelToLocales(excelPath, outputDirectory) {
    debugger;
    const workbook = XLSX.readFile(excelPath);
    const sheet = workbook.Sheets[workbook.SheetNames[0]];
    const json = XLSX.utils.sheet_to_json(sheet);
    WriteToLocales.mapJSONToTree(
      json.sort((a, b) => a.key.localeCompare(b.key))
    );
    WriteToLocales.locales.forEach((locale, localeName) => {
      writeFileSync(
        `${outputDirectory}/${localeName}.json`,
        JSON.stringify(locale, null, 2)
      );
    });
  }

  static mapJSONToTree(json) {
    WriteToLocales.locales = new Map(); // This is to hold each local object
    const tree = {};
    json.forEach((row) => {
      WriteToLocales.mapPropertiesToObject(row, tree);
    });
  }

  static mapPropertiesToObject(row) {
    const propPath = row.key.split(".");
    const locales = Object.keys(row);
    locales.shift();
    locales.forEach((locale) => {
      if (WriteToLocales.locales.get(locale) === undefined) {
        WriteToLocales.locales.set(locale, {});
      }
      const dataObject = WriteToLocales.locales.get(locale);
      let refObj = dataObject;
      propPath.forEach((prop, index) => {
        if (refObj[prop] === undefined) {
          if (index === propPath.length - 1) {
            refObj[prop] = row[locale];
          } else {
            refObj[prop] = {};
          }
        }
        refObj = refObj[prop];
      });
    });
  }
}
const args = process.argv.slice(2);
console.log(args);
WriteToLocales.readFromExcelToLocales(args[0], args[1]);
