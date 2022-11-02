using System;
using System.Linq;
using HL7Enumerator;
using SearchCriteria = Automation.Domain.Classes.HL7.SearchCriteria;

namespace Automation.Domain.Operations
{
    public static class HL7Operations
    {
        public static HL7Element ParseHL7(string message, SearchCriteria criteria)
        {
            if (message.Length < 8) throw new ArgumentException("Not a complete HL7 message");
            var messageHeader = message.Substring(0, 8);
            if (messageHeader.Length < 8) throw new ArgumentException("Not a valid HL7 message");

            var separators = ValidatedSeparators(messageHeader);

            var segmentTerminator = Constants.Separators[0];
            if (criteria.Segment.Length > 0)
            {
                var segmentRepetition = (criteria.elements[0].Repetition < 2) ? 1 : criteria.elements[0].Repetition;
                var segment = DelimitedString.BoundedBy(message, criteria.Segment, segmentTerminator + "", segmentRepetition);
                if (!criteria.Field.Enabled) return segment; // (implictly cast as element)
                var headerOffset = (Constants.HeaderTypes.Any(h => h.Equals(criteria.Segment))) ? 0 : 1;
                var separator = separators[(int)Constants.HL7Separators.field];
                var field = DelimitedString.Field(segment, "" + separator, criteria.Field.Position + headerOffset);
                if (field.Length == 0) return new HL7Element("", separator, separators);
                if (criteria.Field.Repetition > 1)
                {
                    field = DelimitedString.Field(
                          field,
                          "" + separators[(int)Constants.HL7Separators.repeat],
                          criteria.Field.Repetition
                          );
                }
                if (!criteria.Component.Enabled) return new HL7Element(field, separator, separators);
                separator = Constants.Separators[(int)Constants.HL7Separators.component];
                var component = DelimitedString.Field(field,
                     "" + separator,
                     criteria.Component.Position);
                var subcomponentseparator = separators[(int)Constants.HL7Separators.subcomponent];
                return (component.Length == 0 || !criteria.Subcomponent.Enabled) ?
                    new HL7Element(component, separator, separators)
                    :
                    new HL7Element(
                     DelimitedString.Field(field,
                      "" + subcomponentseparator,
                      criteria.Subcomponent.Position), subcomponentseparator, separators);

            }
            return null;
        }

        /// <summary>
        /// Extracts and validates the separator chars from a HL7 message.
        /// Throws an exception if a MSH, BHS, or FHS has invalid Separator chars OR 
        /// Returns the HL7 Standard characters if no header is present.
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        private static string ValidatedSeparators(string header)
        {
            var segmentType = header.Substring(0, 3);
            string result = Constants.Separators;
            if (Constants.HeaderTypes.Any(h => h.Equals(segmentType)))
            {
                result = "" + '\r' + header[3] + header[5] + header[4] + header[7] + header[6];
                var distinctResult = "";
                foreach (char c in result)
                {
                    if (distinctResult.IndexOf(c) > 0)
                    {
                        throw new ArgumentException("Message has invalid separator character definition");
                    }
                    else
                    {
                        distinctResult = distinctResult + c;
                    }
                }
            }
            return result;
        }
    }
}
