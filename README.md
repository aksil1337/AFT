# Automated Functional Tests

AFT is simply my personal test sandbox. It will be made up of several projects, using different languages and frameworks.

## Selenium (C#)

Project uses .NET Framework 4.6.1, NUnit 3 and WebDriver 3.

### Omada

1. Open "omada.net".
   - Make sure the page is loaded properly.

2. Execute search for "gartner".
   - Verify that the search gives more than 1 result and that there is "Gartner IAM Summit 2016   - London" among those.

3. Click on the "Gartner IAM Summit 2016   - London" link.
   - Check you're redirected to the particular article and that the page is loaded properly.

4. Navigate to News (More… > News & Events > News).
   - Verify that the same article is present there.

5. Navigate to the home page, then open Contact.
   - On opened page click U.S West and check if there is class change on this element.
   - On the same page do a mouse hover on different location and take a screenshot before and after performing the action.

6. Open Read Privacy Policy in another tab.
   - Check if it is opened and loaded properly.

7. Click on Close button for Cookie and Privacy Policy ("cookiebar") on previous tab.
   - Close tab with Privacy Policy displayed.
   - Check if Privacy Policy will be not shown anymore on the site.
