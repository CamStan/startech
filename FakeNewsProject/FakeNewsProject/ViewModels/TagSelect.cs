using FakeNewsProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FakeNewsProject.ViewModels
{
    /// <summary>
    /// Viewmodel to apply a boolean to each tag in order to use them as a checkbox
    /// </summary>
    public class TagSelect
    {
        public string TagName { get; set; }
        public int TagID { get; set; }
        public bool IsSelected { get; set; }
    }
}