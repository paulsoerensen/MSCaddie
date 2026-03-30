using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSCaddie.Repository.Models;

public class DiningInfo
{
    public int Dining { set; get; } = 0;
    public int NotDining { set; get; } = 0;
    public string Text => $"{Dining}/{NotDining}";
}
