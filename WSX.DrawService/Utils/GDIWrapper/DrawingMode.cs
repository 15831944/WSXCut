namespace WSX.DrawService.Utils
{
    public enum DrawingMode
    {
        R2_BLACK = 1,   /*  0       */
        R2_NOTMERGEPEN = 2,   /* DPon     */
        R2_MASKNOTPEN = 3,   /* DPna     */
        R2_NOTCOPYPEN = 4,   /* PN       */
        R2_MASKPENNOT = 5,   /* PDna     */
        R2_NOT = 6,   /* Dn       */
        R2_XORPEN = 7,   /* DPx      */
        R2_NOTMASKPEN = 8,   /* DPan     */
        R2_MASKPEN = 9,   /* DPa      */
        R2_NOTXORPEN = 10,  /* DPxn     */
        R2_NOP = 11,  /* D        */
        R2_MERGENOTPEN = 12,  /* DPno     */
        R2_COPYPEN = 13,  /* P        */
        R2_MERGEPENNOT = 14,  /* PDno     */
        R2_MERGEPEN = 15,  /* DPo      */
        R2_WHITE = 16,  /*  1       */
        R2_LAST = 16
    }
}
