using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthInsurance.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Azure.Core;

namespace HealthInsurance.Controllers
{
    public class PoliciesController : Controller
    {
        private readonly HealthInsuranceContext _context;

        public PoliciesController(HealthInsuranceContext context)
        {
            _context = context;
        }




        // GET: Policies
        public async Task<IActionResult> Index()
        {
            return _context.Policies != null ?
                        View(await _context.Policies.ToListAsync()) :
                        Problem("Entity set 'HealthInsuranceContext.Policies'  is null.");
        }

        // GET: Policies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Policies == null)
            {
                return NotFound();
            }

            var policy = await _context.Policies
                .FirstOrDefaultAsync(m => m.PolicyId == id);
            if (policy == null)
            {
                return NotFound();
            }

            return View(policy);
        }

        // GET: Policies/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Policies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PolicyId,PolicyName,CompanyId,PolicyDescription,Amount,Emi")] Policy policy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(policy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // PoliciesController.cs


            return View(policy);
        }



        // GET: Policies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Policies == null)
            {
                return NotFound();
            }

            var policy = await _context.Policies.FindAsync(id);
            if (policy == null)
            {
                return NotFound();
            }
            return View(policy);
        }

        // POST: Policies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PolicyId,PolicyName,CompanyId,PolicyDescription,Amount,Emi")] Policy policy)
        {
            if (id != policy.PolicyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicyExists(policy.PolicyId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(policy);
        }

        // GET: Policies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Policies == null)
            {
                return NotFound();
            }

            var policy = await _context.Policies
                .FirstOrDefaultAsync(m => m.PolicyId == id);
            if (policy == null)
            {
                return NotFound();
            }

            return View(policy);
        }

        // POST: Policies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Policies == null)
            {
                return Problem("Entity set 'HealthInsuranceContext.Policies'  is null.");
            }
            var policy = await _context.Policies.FindAsync(id);
            if (policy != null)
            {
                _context.Policies.Remove(policy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolicyExists(int id)
        {
            return (_context.Policies?.Any(e => e.PolicyId == id)).GetValueOrDefault();
        }









        [HttpPost]
        public async Task<IActionResult> Buy(PoliciesReqDetail policiesEmp)
        {
            if (policiesEmp == null)
            {
                return View(policiesEmp);
            }
            if (ModelState.IsValid)
            {

                _context.PoliciesReqDetails.Add(policiesEmp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(RequestDetails));
            }
            return RedirectToAction("RequestDetails", "Policies", new { id = policiesEmp.RequestId });
        }

        public IActionResult Buy()
        {
            return View();
        }







        [HttpPost]
        public async Task<IActionResult> RequestDetail()
        {
            var policiesReqDetails = await _context.PoliciesReqDetails.ToListAsync();
            if (policiesReqDetails == null)
            {
                return Problem("Entity set 'HealthInsuranceContext.PoliciesReqDetails' is null.");
            }

            return View("RequestDetails", policiesReqDetails);
        }

        public IActionResult RequestDetails()
        {
            return View(_context.PoliciesReqDetails);
        }











        // GET: Policies/Adjust/5
        public async Task<IActionResult> Adjust(int? id)
        {
            if (id == null || _context.PoliciesReqDetails == null)
            {
                return NotFound();
            }

            var policy = await _context.PoliciesReqDetails.FindAsync(id);
            if (policy == null)
            {
                return NotFound();
            }

            ViewBag.Policy = policy; // truyền đối tượng policy vào ViewBag

            return View(policy);
        }

        // POST: Policies/Adjust/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adjust(int? id, [Bind("RequestId,RequestDate,EmployeeId,Status,Emi,PolicyId,PolicyName,PolicyAmount,CompanyId,CompanyName")] PoliciesReqDetail policy)
        {
            if (id != policy.RequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policy);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(RequestDetails), new { id = policy.RequestId });

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicyExists(policy.RequestId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(policy);
        }
    }
}









   





