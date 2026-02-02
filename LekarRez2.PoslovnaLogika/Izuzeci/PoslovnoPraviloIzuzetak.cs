using System;

namespace LekarRez2.PoslovnaLogika.Izuzeci
{
    /// <summary>
    /// Specijalan izuzetak koji označava kršenje poslovnih pravila.
    /// </summary>
    public class PoslovnoPraviloIzuzetak : ApplicationException
    {
        public PoslovnoPraviloIzuzetak(string poruka) : base(poruka)
        {
        }
    }
}
