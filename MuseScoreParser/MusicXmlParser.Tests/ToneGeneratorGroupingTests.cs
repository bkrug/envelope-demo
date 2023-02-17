using NUnit.Framework;

namespace MusicXmlParser.Tests
{
    public class ToneGeneratorGroupingTests
    {
        [Test]
        public void GroupByGenerator_OnePartOneVoiceNoChords_Success()
        {

        }

        [Test]
        public void GroupByGenerator_TwoPartsNoChords_Success()
        {

        }

        [Test]
        public void GroupByGenerator_TwoVoicesNoChords_Success()
        {

        }

        [Test]
        public void GroupByGenerator_FourVoicesNoChords_NoneEmpty_OneVoiceIsIgnored()
        {
        }

        [Test]
        public void GroupByGenerator_FourVoicesNoChords_DifferentVoicesAreAllRestsInDifferentMeasures_SkipAllRestVoicesForGivenMeasure()
        {
        }

        [Test]
        public void GroupByGenerator_OneVoicesWithChords_IncludeLowerNotesOfChordsInSecondAndThirdGenerators()
        {
        }

        [Test]
        public void GroupByGenerator_TwoVoicesWithChords_IncludeLowerNotesOfChordsInThirdGenerator()
        {
        }

        [Test]
        public void GroupByGenerator_ThreeVoicesWithChords_IgnoreLowerNotesOfChords()
        {
        }
    }
}