using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace CohortAnalysis
{
    public class HtmlReportGenerator
    {
        private string _filename;

        private const string Styles = @"body {
		font-family: Arial, sans-serif;
		color: #444;
	}
	table, th, td {
		border: 1px solid black;
		border-collapse: collapse;
	}
	th, td {
		white-space: nowrap;
		padding: 15px;
		margin: 0;
	}
";

        public HtmlReportGenerator(string filename)
        {
            _filename = filename;
        }

        public void WriteReport(ICollection<ICustomerOrderDataPoint> data)
        {
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true
            };

            XDocument html = GenerateReport(data);

            using (var writer = XmlWriter.Create(_filename, settings))
            {
                html.WriteTo(writer);
            }
        }

        private XDocument GenerateReport(ICollection<ICustomerOrderDataPoint> data)
        {
            var html = new XDocument(
                new XDocumentType("html", null, null, null),
                new XElement("html",
                    new XElement("head",
                        new XElement("style", Styles)
                    ),
                    new XElement("body",
                        new XElement("table",
                            new XElement("thead",
                                new XElement("tr",
                                    new XElement("td", "Cohort"),
                                    new XElement("td", "Customers")
                                )
                            ),
                            new XElement("tbody")
                        )
                    )
                )
            );

            var thead = html.Descendants("thead").First();

            BuildTableHead(thead, data);

            var tbody = html.Descendants("tbody").First();

            BuildTableBody(tbody, data);

            return html;
        }

        private void BuildTableHead(XElement thead, ICollection<ICustomerOrderDataPoint> data)
        {
            int maxBuckets = data.Select(d => d.CohortPeriod).Max();

            var headerRow = thead.Descendants("tr").First();

            for (int bucketNumber = 0; bucketNumber < maxBuckets; bucketNumber++)
            {
                int bucketStartDay = bucketNumber * 7;
                string columnHeading = string.Format("{0}-{1} days",
                    bucketStartDay, bucketStartDay + 6);

                headerRow.Add(new XElement("td", columnHeading));
            }
        }

        private void BuildTableBody(XElement tbody, ICollection<ICustomerOrderDataPoint> data)
        {
            var cohorts = data
                .Select(d => new { CohortIdentifier = d.CohortIdentifier })
                .Distinct()
                .OrderBy(d => d.CohortIdentifier);

            foreach (var cohort in cohorts)
            {
                tbody.Add(BuildCohortRow(cohort.CohortIdentifier, data));
            }
        }

        private XElement BuildCohortRow(string cohortIdentifier, ICollection<ICustomerOrderDataPoint> data)
        {
            var row = new XElement("tr",
                new XElement("td", cohortIdentifier)
            );

            // add customers in cohort
            var customersInCohort = data.Where(d => d.CohortIdentifier == cohortIdentifier)
                .Select(d => new { CustomerId = d.Id }).Distinct();

            string totalString = string.Format("{0} customers",
                customersInCohort.Count());

            row.Add(new XElement("td", totalString));

            BuildBuckets(row, cohortIdentifier, customersInCohort.Count(), data);

            return row;
        }

        private void BuildBuckets(XElement row, string cohortIdentifier, int customersInCohort, ICollection<ICustomerOrderDataPoint> data)
        {
            int totalOrders = data.Count();

            int maxBucket = data.Select(d => d.CohortPeriod).Max();

            for (int bucket = 1; bucket < maxBucket; bucket++)
            {
                var ordersInBucket = data
                    .Where(d => d.CohortIdentifier == cohortIdentifier && 
                                d.CohortPeriod == bucket);

                int firstOrdersInBucket = 0;
                foreach (var dataPoint in ordersInBucket)
                {
                    if (IsFirstOrder(dataPoint, data))
                    {
                        firstOrdersInBucket++;
                    }
                }

                if (ordersInBucket.Count() > 0)
                {
                    string orderersInBucketString =
                                string.Format("{0}% orderers ({1})",
                                              (int) (((float)ordersInBucket.Count() / customersInCohort) * 100),
                                              ordersInBucket.Count());

                    string firstOrdersString = string.Format("{0}% 1st time ({1})",
                        (int)(((float)firstOrdersInBucket / customersInCohort) * 100),
                        firstOrdersInBucket);

                    row.Add(new XElement("td", 
                        new XElement("p", orderersInBucketString),
                        new XElement("p", firstOrdersString)
                    )); 
                }
                else
                {
                    row.Add(new XElement("td", string.Empty));
                }
            }
        }

        private bool IsFirstOrder(ICustomerOrderDataPoint dataPoint, ICollection<ICustomerOrderDataPoint> data)
        {
            var firstOrderForCustomer = data.Where(d => d.Id == dataPoint.Id)
                .OrderBy(d => d.OrderDate)
                .FirstOrDefault();

            return firstOrderForCustomer != null &&
                firstOrderForCustomer.OrderDate == dataPoint.OrderDate;
            {

            }
        }
    }
}
