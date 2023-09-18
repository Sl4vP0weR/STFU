global using Convert = System.Convert;
global using System;
global using System.Data;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.Collections;
global using System.Collections.Generic;
global using System.Globalization;
global using System.Net;
global using System.Text;
global using System.Text.RegularExpressions;
global using System.Runtime.CompilerServices;

global using Serilog;
global using Serilog.Core;
global using Serilog.Settings.Configuration;
global using ILogger = Serilog.ILogger;

global using Sentry;

global using Swashbuckle.AspNetCore;
global using Swashbuckle.AspNetCore.Annotations;

global using Microsoft.Net.Http.Headers;
global using Microsoft.Extensions.Caching.Memory;
global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.Primitives;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Design;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.EntityFrameworkCore.ChangeTracking;
global using Microsoft.EntityFrameworkCore.ValueGeneration;

global using Microsoft.AspNetCore.Diagnostics;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.AspNetCore.OutputCaching;
global using HttpMethod = Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpMethod;

global using static STFU.Extensions;
global using static STFU.Startup;

global using STFU.Controllers;
global using STFU.DTO;
global using STFU.DataBase;
global using STFU.Entities;
global using STFU.Exceptions;
global using STFU.Filters;
global using STFU.Services;
global using STFU.Settings;