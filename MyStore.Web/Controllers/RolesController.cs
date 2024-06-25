﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MyStore.Web.Controllers;

public class RolesController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RolesController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var roles = _roleManager.Roles;
        return View(roles);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(IdentityRole model)
    {
        if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
        {
            _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
        }

        return RedirectToAction("Index");
    }
}
