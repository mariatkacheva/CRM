#pragma checksum "C:\Users\Мария Ткачёва\Desktop\КУРСАЧ\LDanceCRMRazorPages3\LDanceCRMRazorPages3\Pages\TimetableBuy.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2bad0e31870536811d908d0f333f3a0d89235937"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(LDanceCRMRazorPages3.Pages.Pages_TimetableBuy), @"mvc.1.0.razor-page", @"/Pages/TimetableBuy.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2bad0e31870536811d908d0f333f3a0d89235937", @"/Pages/TimetableBuy.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"496cdfda58263ac2c045d4088b4424670fc945e4", @"/Pages/_ViewImports.cshtml")]
    #nullable restore
    public class Pages_TimetableBuy : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n");
#nullable restore
#line 7 "C:\Users\Мария Ткачёва\Desktop\КУРСАЧ\LDanceCRMRazorPages3\LDanceCRMRazorPages3\Pages\TimetableBuy.cshtml"
 if (!string.IsNullOrEmpty(Model.paymentErrorMessage))
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <script>
        $(document).ready(function () {
            // Определяем функцию для отображения всплывающего сообщения
            function showNotification(message) {
                // Создаем элемент сообщения
                var notification = $('<div class=""notification"">' + message + '</div>');

                // Стилизуем сообщение
                notification.css({
                    'position': 'fixed',
                    'top': '50px',
                    'right': '10px',
                    'background-color': '#fcd642',
                    'color': '#ffffff',
                    'padding': '20px',
                    'border-radius': '5px',
                    'font-size': '15px',
                    'z-index': '9999'
                });

                // Добавляем сообщение в тело документа
                $('body').append(notification);

                // Задаем таймер для автоматического скрытия сообщения через 5 секунд
                setTimeout(function ()");
            WriteLiteral(@" {
                    notification.fadeOut('slow', function () {
                        $(this).remove();
                    });
                }, 2500);
            }

            // Пример вызова функции с сообщением
            showNotification('В настоящий момент онлайн оплата недоступна.');
        });
    </script>
");
#nullable restore
#line 44 "C:\Users\Мария Ткачёва\Desktop\КУРСАЧ\LDanceCRMRazorPages3\LDanceCRMRazorPages3\Pages\TimetableBuy.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

    <div class=""text-center mb-3"">
        <h3>Для посещения этого занятия необходимо приобрести абонемент.</h3>
    </div>

    <div class=""container"">
        <div class=""row justify-content-center"">
            <div class=""col-md-6"">
                <div class=""mt-4"">
");
            WriteLiteral("                    <table class=\"table\">\r\n                        <tbody>\r\n                            <tr>\r\n                                <td style=\"width: 50%;\"><strong>Название:</strong></td>\r\n                                <td style=\"width: 50%;\">");
#nullable restore
#line 60 "C:\Users\Мария Ткачёва\Desktop\КУРСАЧ\LDanceCRMRazorPages3\LDanceCRMRazorPages3\Pages\TimetableBuy.cshtml"
                                                   Write(Model.passtypeInfo.PassTypeName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <td><strong>Стоимость:</strong></td>\r\n                                <td>");
#nullable restore
#line 64 "C:\Users\Мария Ткачёва\Desktop\КУРСАЧ\LDanceCRMRazorPages3\LDanceCRMRazorPages3\Pages\TimetableBuy.cshtml"
                               Write(Model.passtypeInfo.PassTypePrice);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            </tr>\r\n                        </tbody>\r\n                    </table>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2bad0e31870536811d908d0f333f3a0d892359376949", async() => {
                WriteLiteral(@"
    <div class=""row mb-3"">

        <div class=""offset-sm-3 col-sm-3 d-grid"">
            <button type=""submit"" class=""btn btn-primary btn-sm w-100 custom-bg-color""><i class=""fas fa-money-bill""></i> Купить абонемент</button>
        </div>

        <div class=""col-sm-3 d-grid"">
            <a class=""btn btn-outline-primary btn-sm""");
                BeginWriteAttribute("href", " href=\"", 2950, "\"", 2996, 2);
                WriteAttributeValue("", 2957, "/Timetable?searchDate=", 2957, 22, true);
#nullable restore
#line 81 "C:\Users\Мария Ткачёва\Desktop\КУРСАЧ\LDanceCRMRazorPages3\LDanceCRMRazorPages3\Pages\TimetableBuy.cshtml"
WriteAttributeValue("", 2979, Model.SearchDate, 2979, 17, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" role=\"button\">Назад</a>\r\n        </div>\r\n\r\n    </div>\r\n    ");
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
            WriteLiteral("\r\n    \r\n\r\n\r\n\r\n    \r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LDanceCRMRazorPages3.Pages.TimetableBuyModel> Html { get; private set; } = default!;
        #nullable disable
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<LDanceCRMRazorPages3.Pages.TimetableBuyModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<LDanceCRMRazorPages3.Pages.TimetableBuyModel>)PageContext?.ViewData;
        public LDanceCRMRazorPages3.Pages.TimetableBuyModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591