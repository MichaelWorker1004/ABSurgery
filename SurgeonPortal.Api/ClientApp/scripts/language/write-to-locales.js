import fs, { writeFileSync } from "fs";
import * as process from "process";
import path from "path";
import * as XLSX from "xlsx";
import { Readable } from "stream";
import { fileURLToPath } from "url";

/**
 * This project uses SheetJS to read and write excel files
 * [SheetJS](https://sheetjs.com/)
 * [SheetJS GitHub](https://github.com/SheetJS/sheetjs)
 * [SheetJS Docs for node.js](https://docs.sheetjs.com/docs/getting-started/installation/nodejs)
 */

// A JS Module way to do the node.js __dirname
const __dirname = path.dirname(fileURLToPath(import.meta.url));

XLSX.set_fs(fs); // DO NOT FORGET THIS LINE! (required for use with raw data)
XLSX.stream.set_readable(Readable); // DO NOT FORGET THIS LINE! (required for use with raw data)

/**
 * A class to read from an excel file and write to a set of locale files
 */
export class WriteToLocales {
  /**
   * A map of locale objects
   * @type {Map<string, object>}
   */
  static locales;

  /**
   * Read from an excel file and write to a set of locale files
   * @param {string} excelPath - The path to the excel file
   * @param {string} outputDirectory - The path to the output directory where the locale files will be written
   */
  static readFromExcelToLocales(excelPath, outputDirectory) {
    debugger;
    const workbook = XLSX.readFile(excelPath);
    const sheet = workbook.Sheets[workbook.SheetNames[0]];
    const json = XLSX.utils.sheet_to_json(sheet);
    // Take the JSON from the spreadsheet and map it to a tree (the static locales property)
    // Excel and many data table data comes as an array of objects
    // The translations files are in a tree structure, the key is used to build that structure
    // through a recursive function
    // The json is sorted by key so that the tree is built in a consistent way
    // This allows for easy comparison of the tree to the tree in the locale files
    WriteToLocales.mapJSONToTree(
      json.sort((a, b) => a.key?.localeCompare(b.key))
    );
    // Write the locale files to the output directory
    WriteToLocales.locales.forEach((locale, localeName) => {
      writeFileSync(
        `${outputDirectory}/${localeName}.json`,
        JSON.stringify(locale, null, 2)
      );
    });
  }

  /**
   * Map the JSON from the spreadsheet to a tree
   * @param json
   */
  static mapJSONToTree(json) {
    WriteToLocales.locales = new Map(); // This is to hold each local object
    const tree = {};
    json.forEach((row) => {
      WriteToLocales.mapPropertiesToObject(row, tree);
    });
  }

  /**
   * Map the properties of the row to the tree
   * @param row
   */
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

// This is the entry point for the script
const args = process.argv.slice(2); // The first two arguments are the node executable and the script file so exclude them
// Extract the arguments into named constants for clarity
const excelPath = args[0];
const outputDirectory = args[1];

// Call the readFromExcelToLocales function
WriteToLocales.readFromExcelToLocales(excelPath, outputDirectory);
