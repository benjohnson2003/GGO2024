using System;

public class Timer
{
    private float duration;
    private float elapsed;
    public event Action onTimerComplete; // Function called when timer is complete

    bool repeating = false;

    public Timer(float duration, bool repeating = false)
    {
        this.duration = duration;
        this.repeating = repeating;

        elapsed = 0;
    }

    public Timer(float duration, bool repeating, Action onTimerComplete) : this(duration, repeating)
    {
        this.onTimerComplete = onTimerComplete;
    }

    public void Tick(float deltaTime)
    {
        if (elapsed == duration)
            return; // Timer is already complete, do not update

        elapsed += deltaTime;

        if (elapsed > duration)
        {
            // Timer complete
            elapsed = repeating ? 0 : duration;
            onTimerComplete?.Invoke();
        }
    }

    // Returns time remaining in seconds
    public float TimeRemaining()
    {
        return duration - elapsed;
    }
}
