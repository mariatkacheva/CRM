#pragma checksum "C:\Users\Мария Ткачёва\Desktop\КУРСАЧ\LDanceCRMRazorPages3\LDanceCRMRazorPages3\Pages\PassTypes.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c559c767b712b929a91730ba3f21e1c5c9016092"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(LDanceCRMRazorPages3.Pages.Pages_PassTypes), @"mvc.1.0.razor-page", @"/Pages/PassTypes.cshtml")]
namespace LDanceCRMRazorPages3.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Мария Ткачёва\Desktop\КУРСАЧ\LDanceCRMRazorPages3\LDanceCRMRazorPages3\Pages\_ViewImports.cshtml"
using LDanceCRMRazorPages3;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c559c767b712b929a91730ba3f21e1c5c9016092", @"/Pages/PassTypes.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"496cdfda58263ac2c045d4088b4424670fc945e4", @"/Pages/_ViewImports.cshtml")]
    #nullable restore
    public class Pages_PassTypes : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<br />\r\n<h2>Виды абонементов</h2>\r\n\r\n");
            WriteLiteral("<div style=\"margin-top: 10px;\">\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c559c767b712b929a91730ba3f21e1c5c90160923566", async() => {
                WriteLiteral("\r\n        <div class=\"input-group mb-3\" style=\"width: 50%;\">\r\n            <input type=\"text\" class=\"form-control\" name=\"searchString\" placeholder=\"Введите название абонемента или тренировки...\"");
                BeginWriteAttribute("value", " value=\"", 365, "\"", 392, 1);
#nullable restore
#line 13 "C:\Users\Мария Ткачёва\Desktop\КУРСАЧ\LDanceCRMRazorPages3\LDanceCRMRazorPages3\Pages\PassTypes.cshtml"
WriteAttributeValue("", 373, Model.SearchString, 373, 19, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@">
            <div class=""input-group-append"" style=""margin-left: 10px;"">
                <button class=""btn btn-primary"" type=""submit"">
                    <i class=""fas fa-search""></i>
                </button>
                <a class=""btn btn-secondary btn-custom"" href=""/PassTypes"">
                    <i class=""fas fa-sync-alt""></i>
                </a>
            </div>
        </div>
    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n\r\n");
#nullable restore
#line 27 "C:\Users\Мария Ткачёва\Desktop\КУРСАЧ\LDanceCRMRazorPages3\LDanceCRMRazorPages3\Pages\PassTypes.cshtml"
 if (Model.errorMessage.Length > 0)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"row mb-3 mt-3\">\r\n        <div class=\"col-sm-9\">\r\n            <div class=\"alert alert-info  alert-dismissible fade show\" role=\"alert\">\r\n                <strong>");
#nullable restore
#line 32 "C:\Users\Мария Ткачёва\Desktop\КУРСАЧ\LDanceCRMRazorPages3\LDanceCRMRazorPages3\Pages\PassTypes.cshtml"
                   Write(Model.errorMessage);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong>\r\n                <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"Close\"></button>\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 37 "C:\Users\Мария Ткачёва\Desktop\КУРСАЧ\LDanceCRMRazorPages3\LDanceCRMRazorPages3\Pages\PassTypes.cshtml"
}
else
{
    

#line default
#line hidden
#nullable disable
            WriteLiteral("    <table class=\"table\">\r\n        <thead>\r\n            <tr>\r\n");
            WriteLiteral("                <th>Тренировка</th>\r\n                <th>Количество посещений</th>\r\n                <th>Стоимость (руб.)</th>\r\n                <th>Действия</th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
#nullable restore
#line 52 "C:\Users\Мария Ткачёва\Desktop\КУРСАЧ\LDanceCRMRazorPages3\LDanceCRMRazorPages3\Pages\PassTypes.cshtml"
             foreach (var item in Model.passtypesList)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n");
            WriteLiteral("                    <td>");
#nullable restore
#line 56 "C:\Users\Мария Ткачёва\Desktop\КУРСАЧ\LDanceCRMRazorPages3\LDanceCRMRazorPages3\Pages\PassTypes.cshtml"
                   Write(item.TrainingName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 57 "C:\Users\Мария Ткачёва\Desktop\КУРСАЧ\LDanceCRMRazorPages3\LDanceCRMRazorPages3\Pages\PassTypes.cshtml"
                   Write(item.PassTypeNumberOfVisits);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 58 "C:\Users\Мария Ткачёва\Desktop\КУРСАЧ\LDanceCRMRazorPages3\LDanceCRMRazorPages3\Pages\PassTypes.cshtml"
                   Write(item.PassTypePrice);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 60 "C:\Users\Мария Ткачёва\Desktop\КУРСАЧ\LDanceCRMRazorPages3\LDanceCRMRazorPages3\Pages\PassTypes.cshtml"
                     if(item.isSold)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        <td>
                            <a class=""btn btn-success-custom btn-sm w-100"">
                                <i class=""fas fa-check"" style=""color: #ffffff;""></i> Абонемент активен
                            </a>
                        </td>                                            
");
#nullable restore
#line 67 "C:\Users\Мария Ткачёва\Desktop\КУРСАЧ\LDanceCRMRazorPages3\LDanceCRMRazorPages3\Pages\PassTypes.cshtml"
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <td>\r\n                            <a class=\"btn btn-primary btn-sm w-100 custom-bg-color\"");
            BeginWriteAttribute("href", " href=\"", 2536, "\"", 2620, 4);
            WriteAttributeValue("", 2543, "/PassTypeBuy?searchString=", 2543, 26, true);
#nullable restore
#line 71 "C:\Users\Мария Ткачёва\Desktop\КУРСАЧ\LDanceCRMRazorPages3\LDanceCRMRazorPages3\Pages\PassTypes.cshtml"
WriteAttributeValue("", 2569, Model.SearchString, 2569, 19, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2588, "&amp;PassTypeID=", 2588, 16, true);
#nullable restore
#line 71 "C:\Users\Мария Ткачёва\Desktop\КУРСАЧ\LDanceCRMRazorPages3\LDanceCRMRazorPages3\Pages\PassTypes.cshtml"
WriteAttributeValue("", 2604, item.PassTypeID, 2604, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                <i class=\"fas fa-money-bill\"></i> Купить абонемент\r\n                            </a>\r\n                        </td>\r\n");
#nullable restore
#line 75 "C:\Users\Мария Ткачёва\Desktop\КУРСАЧ\LDanceCRMRazorPages3\LDanceCRMRazorPages3\Pages\PassTypes.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tr>\r\n");
#nullable restore
#line 77 "C:\Users\Мария Ткачёва\Desktop\КУРСАЧ\LDanceCRMRazorPages3\LDanceCRMRazorPages3\Pages\PassTypes.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n");
#nullable restore
#line 80 "C:\Users\Мария Ткачёва\Desktop\КУРСАЧ\LDanceCRMRazorPages3\LDanceCRMRazorPages3\Pages\PassTypes.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LDanceCRMRazorPages3.Pages.PassTypesModel> Html { get; private set; } = default!;
        #nullable disable
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<LDanceCRMRazorPages3.Pages.PassTypesModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<LDanceCRMRazorPages3.Pages.PassTypesModel>)PageContext?.ViewData;
        public LDanceCRMRazorPages3.Pages.PassTypesModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591