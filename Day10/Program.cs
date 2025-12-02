using System;
using System.Collections.Generic;
using System.Linq;
using static System.IO.File;

var inputPath = Helpers.Helpers.InputPath();
var input = (await ReadAllLinesAsync(inputPath))[0];