using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace TraceIt.Extensions
{
    public static class SearchBarExtensions
    {
        public static void CheckForInvalidCharacters(this SearchBar searchBar)
        {
            string text = searchBar.Text;

            Regex invalidCharacters = new Regex("[%',^*()@!|{}<>/]");
            searchBar.Text = invalidCharacters.Replace(text, "");

            Debug.WriteLine(searchBar.Text);
        }
    }
}
