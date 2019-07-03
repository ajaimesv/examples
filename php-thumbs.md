# Thumbnail creation using PHP

```
#!/usr/bin/php

<?php

$cmd = "ffmpeg -i 1245.wmv -f image2 -vf \"select='eq(pict_type,PICT_TYPE_I)',showinfo\" -vsync vfr thumb-%03d.png";
exec($cmd);

$i = 1;
$winning = [];
while ($i < 327) {
    echo "---------------------------------\n";
    echo "Starting Main loop, i: $i\n";

	$first = str_pad($i, 3, '0', STR_PAD_LEFT);
	$second = str_pad($i + 1, 3, '0', STR_PAD_LEFT);

    echo "First: $first\n";
    echo "Second: $second\n";

	$output = [];
	$cmd = "compare -metric phash -fuzz 15% thumb-$first.png thumb-$second.png compare.png 2>&1";
	exec($cmd, $output);
	$ratio1 = floatval($output[0]);

    echo "ratio1: $ratio1\n";

    if ($ratio1 > 100) {
        array_push($winning, $first);
        copy("thumb-$first.png", "final/thumb-$first.png");
        $i++;
        echo "Next i: $i\n";
    } else {
        $third = str_pad($i + 2, 3, '0', STR_PAD_LEFT);

        echo "Third: $third\n";

        $output = [];
        $cmd = "compare -metric phash -fuzz 15% thumb-$second.png thumb-$third.png compare.png 2>&1";
        exec($cmd, $output);
        $ratio3 = floatval($output[0]);

        echo "ratio3: $ratio3\n";

        if ($ratio3 < 100) {
            $output = [];
            $cmd = "convert thumb-$first.png -colorspace Gray -format \"%[fx:quantumrange*image.mean]\" info:";
            exec($cmd, $output);
            $lum1 = floatval($output[0]);

            echo "lum1: $lum1\n";

            $output = [];
            $cmd = "convert thumb-$second.png -colorspace Gray -format \"%[fx:quantumrange*image.mean]\" info:";
            exec($cmd, $output);
            $lum2 = floatval($output[0]);

            echo "lum2: $lum2\n";

            $output = [];
            $cmd = "convert thumb-$third.png -colorspace Gray -format \"%[fx:quantumrange*image.mean]\" info:";
            exec($cmd, $output);
            $lum3 = floatval($output[0]);

            echo "lum3: $lum3\n";

            if ($lum1 >= $lum2 && $lum1 >= $lum3) {
                echo "adding: $first\n";
                $winner = $first;
                array_push($winning, $first);
                copy("thumb-$first.png", "final/thumb-$first.png");
            } else if ($lum2 >= $lum1 && $lum2 >= $lum3) {
                echo "adding: $second\n";
                $winner = $second;
                array_push($winning, $second);
                copy("thumb-$second.png", "final/thumb-$second.png");
            } else if ($lum3 >= $lum1 && $lum3 >= $lum2) {
                echo "adding: $third\n";
                $winner = $third;
                array_push($winning, $third);
                copy("thumb-$third.png", "final/thumb-$third.png");
            }

            $i += 2;
            $j = 1;
            $diff = 0;
            $first = str_pad($i, 3, '0', STR_PAD_LEFT);
            echo "Entering loop\n";
            while ($diff < 100) {
                $second = str_pad($i + $j, 3, '0', STR_PAD_LEFT);

                echo "First: $first\n";
                echo "Second: $second\n";

                if (file_exists("thumb-$first.png") && file_exists("thumb-$second.png")) {
                    $output = [];
                    $cmd = "compare -metric phash -fuzz 15% thumb-$first.png thumb-$second.png compare.png 2>&1";
                    exec($cmd, $output);
                    $diff = floatval($output[0]);

                    echo "diff: $diff\n";

                    $j++;
                } else break;
            }
            $i = $i + $j - 1;
            echo "Exiting loop i: $i\n";
        } else {
            $output = [];
            $cmd = "convert thumb-$first.png -colorspace Gray -format \"%[fx:quantumrange*image.mean]\" info:";
            exec($cmd, $output);
            $lum1 = floatval($output[0]);

            $output = [];
            $cmd = "convert thumb-$second.png -colorspace Gray -format \"%[fx:quantumrange*image.mean]\" info:";
            exec($cmd, $output);
            $lum2 = floatval($output[0]);

            if ($lum1 > $lum2) {
                echo "adding: $first\n";
                array_push($winning, $first);
                copy("thumb-$first.png", "final/thumb-$first.png");
            } else {
                echo "adding: $second\n";
                array_push($winning, $second);
                copy("thumb-$second.png", "final/thumb-$second.png");
            }
            $i += 2;
            echo "Next i: $i\n";
        }
    }


}
print_r($winning);
```
