using Abp.UI;
using ClosedXML.Excel;
using EmailMaketing.Customers;
using EmailMaketing.SenderEmails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Users;

namespace EmailMaketing.Web.Pages.SenderEmails
{
    public class IndexModel : EmailMaketingPageModel
    {
        private readonly ICurrentUser _currentUser;
        private readonly ICustomerRepository _customerRepository;
        private readonly SenderEmailAppService _senderEmailAppService;
        List<CreateUpdateSenderEmailDto> senderEmail = new List<CreateUpdateSenderEmailDto>();

        public IndexModel(ICurrentUser currentUser, ICustomerRepository customerRepository,
            SenderEmailAppService senderEmailAppService)
        {
            _currentUser = currentUser;
            _customerRepository = customerRepository;
            _senderEmailAppService = senderEmailAppService;
        }
        public async Task<IActionResult> OnPostImportAsync(IFormFile excel)
        {
            if (excel == null)
            {
                throw new UserFriendlyException("Execl emty");
            }
            using (var workbook = new XLWorkbook(excel.OpenReadStream()))
            {
                var worksheet = workbook.Worksheet("Users Sheet");
                var count = 0;
                if (_currentUser.UserName != "admin")
                {
                    foreach (var row in worksheet.Rows())
                    {
                        count += 1;
                        var userId = _currentUser.Id; //Lay userId hien tai
                        var customer = await _customerRepository.FindAsync(x => x.UserID == userId);
                        if (count > 1)
                        {
                            senderEmail.Add(new CreateUpdateSenderEmailDto()
                            {
                                Email = row.Cell(1).Value.ToString(),
                                Password = row.Cell(2).Value.ToString(),
                                CustomerID = customer.Id
                            });
                        }
                    }
                }
                else
                {
                    foreach (var row in worksheet.Rows())
                    {
                        count += 1;
                        if (count > 1)
                        {
                            senderEmail.Add(new CreateUpdateSenderEmailDto()
                            {
                                Email = row.Cell(1).Value.ToString(),
                                Password = row.Cell(2).Value.ToString()
                            });
                        }
                    }
                }
            }
            await _senderEmailAppService.CreateManyAsync(senderEmail);
            return NoContent();
        }
    }
}
