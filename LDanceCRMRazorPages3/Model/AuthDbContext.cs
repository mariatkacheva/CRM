using LDanceCRMRazorPages3.Model;
using LDanceCRMRazorPages3.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
//using static LDanceCRMRazorPages3.Model.AuthDbContext;

namespace LDanceCRMRazorPages3.Model
{
    public class AuthDbContext: IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        
    }    
}
