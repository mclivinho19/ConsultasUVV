using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using ConsultasUVV.Data;
using ConsultasUVV.Models;

namespace ConsultasUVV.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        public AccountController(ApplicationDbContext context, IPasswordHasher<Usuario> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Usuario model)
        {
            if (ModelState.IsValid)
            {
                // Verificar se email existe
                if (_context.Usuarios.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Este e-mail já está cadastrado.");
                    return View(model);
                }

                var usuario = new Usuario
                {
                    Nome = model.Nome,
                    Email = model.Email,
                    DataCadastro = DateTime.Now,
                    Senha = _passwordHasher.HashPassword(null!, model.Senha)
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                // login automatico apos cadastro
                await SignInUser(usuario);
                return RedirectToAction("Index", "Consultas");
            }
            return View(model);
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string senha)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                ModelState.AddModelError("", "Informe e-mail e senha.");
                return View();
            }

            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email);
            if (usuario == null)
            {
                ModelState.AddModelError("", "E-mail ou senha inválidos.");
                return View();
            }

            var resultado = _passwordHasher.VerifyHashedPassword(null!, usuario.Senha, senha);
            if (resultado == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "E-mail ou senha inválidos.");
                return View();
            }

            await SignInUser(usuario);
            return RedirectToAction("Index", "Consultas");
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}