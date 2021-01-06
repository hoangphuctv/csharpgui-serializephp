<?php

$filePath = isset($argv[1]) ? $argv[1] : '';
if (!is_file($filePath )) {
    echo "path not found: ", $filePath, "\n";
    exit(1);
}

$text = file_get_contents($filePath);
$text = trim($text);

if (substr($text, 0, 2) == 'a:') {
	$data = @unserialize($text);
	var_export($data);
}elseif (substr($text, 0, 5) == 'array')  {
	$data = '';
    try {
        eval('$data = '.$text.';');
    }catch(Exception $e) {
    }
    echo serialize($data);
}else {
	echo serialize($text);
}
