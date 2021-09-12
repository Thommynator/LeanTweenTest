Hi dentedpixel 👋

First of all, I really like your LeanTween project!! 😀
I noticed some unexpected LeanTween behavior in one of my other projects and tried to reproduce it in this project.

# Setup
* 3 rows of objects, each row is an instance of a prefab
* a row consists of:
	* an UI Image, whose alpha value is changed by LeanTween using `LeanTween.alpha(image.rectTransform, 0.3f, 0.5f).setLoopPingPong()` 
	* an ON button, which triggers the effect `LeanTween.resume(tweenId)`
	* an OFF button, which pauses the effect `LeanTween.pause(tweenId)`
	* a text field, which shows the current `isPaused` value `LeanTween.isPaused(tweenId)`
	* a text field, which shows the current `isTweening` value `LeanTween.isTweening(tweenId)`
	* each row has its own unique tweenId (shown above the image), derived from `tweenId = LeanTween.alpha(image.rectTransform, 0.3f, 0.5f).setLoopPingPong().id`

# Current Behavior
![](blink-test.gif)
* `isTweening` is ALWAYS true, even when the tween is paused (is this expected behavior?)
	* I expected that `isTweening` is the opposite/negation of `isPaused` 
	* The docu says: 
	> Test whether or not a tween is active on a GameObject
	
	And I thought when a tween is paused it's not active, so it would be false.
* when the first row is resumed or paused, then it also changes the value of all the other rows (even though they have different ids, so `LeanTween.isPaused(tweenId)` should be unique for the first row and shouldn't affect the other rows
* when the other rows are resumed or paused, then it does _not_ change the result of the `isPaused` method
* I checked the [LeanTween isPaused() implementation](https://github.com/dentedpixel/LeanTween/blob/master/Assets/LeanTween/Framework/LeanTween.cs#L837) and noticed that `isPaused` returns the result of `isTweening`, is this intended?
```
    public static bool isPaused(RectTransform rect)
    {
        return isTweening(rect.gameObject);
    }
```

# Expected Behavior
* Pausing a tween should not affect the result of `isPaused` for other tweens with different ids.
* `isTweening` returns true, while it's tweening (when active) or false when not (e.g. when paused). But perhaps that's a wrong expectation or understanding on my side.


