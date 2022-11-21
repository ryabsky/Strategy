namespace Strategy.Exceptions
{
    public class ExperimentNotCompletedException : Exception
    {
        public ExperimentNotCompletedException()
        {
        }

        public ExperimentNotCompletedException(string message)
            : base(message)
        {
        }

        public ExperimentNotCompletedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
